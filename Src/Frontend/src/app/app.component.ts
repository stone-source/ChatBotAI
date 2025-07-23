import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewChecked } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from './services/api.service';
import { AiEngineResponseDetailsDto } from './models/ai-response.dto';
import { Observable, Subscription, timer, take, tap, finalize } from 'rxjs';
import { CommonModule, NgClass, NgFor, NgIf } from '@angular/common';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ChangeDetectorRef, NgZone } from '@angular/core';

export interface ChatMessage {
  sender: 'user' | 'bot';
  text: string;
  botData?: AiEngineResponseDetailsDto;
  isRated?: boolean;
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgIf,
    NgFor,
    NgClass,

    MatToolbarModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy, AfterViewChecked {
  @ViewChild('chatContainer') private chatContainer!: ElementRef;

  messages: ChatMessage[] = [];
  userInput = new FormControl('', [Validators.required]);
  isLoading = true;
  isGenerating = false;
  isCanceled = false;
  
  private generationSubscription: Subscription | null = null;
  private typingQueue: Observable<any>[] = [];
  private typingInProgress = false;
  private currentBotMessageLength: number = 0;
  private currentGeneratedBotMessageLength: number = 0;

  constructor(
    private apiService: ApiService,
    private cdr: ChangeDetectorRef,
    private zone: NgZone
) {}

  ngOnInit(): void {
  this.isLoading = true;

  this.apiService.getHistory('2A89FCF3-27A9-481F-8437-007212BFA61E').subscribe({
    next: (data) => {
      this.messages = [];

      for (const item of data) {
        this.messages.push({
          sender: 'user',
          text: item.userRequest!.request,
        });

        this.messages.push({
          sender: 'bot',
          text: item.answer ?? '',
          botData: item,
          isRated: item.rating !== null
        });
      }

      this.isLoading = false;
    },
    error: (err) => {
      this.isLoading = false;
    }
  });
}

  ngAfterViewChecked() {
    this.scrollToBottom();
  }
  
  ngOnDestroy(): void {
    if (this.generationSubscription) {
      this.generationSubscription.unsubscribe();
    }
  }

  handleSendOrCancel(): void {
    if (this.isMessageGenerating) {
      this.cancelGeneration();
    } else {
      this.sendMessage();
    }
  }

  resetLengthCounters(): void {
    this.currentBotMessageLength = 0;
    this.currentGeneratedBotMessageLength = 0;
  }

  get isMessageGenerating(): boolean {
    return (this.currentBotMessageLength !== this.currentGeneratedBotMessageLength) || this.isGenerating;
  }

  private sendMessage(): void {
    if (this.userInput.invalid) return;

    const userText = this.userInput.value!;
    this.messages.push({ sender: 'user', text: userText });
    this.userInput.reset();
    this.isGenerating = true;
    this.isCanceled = false;
    this.resetLengthCounters();

    const botMessageIndex = this.messages.push({ sender: 'bot', text: '' }) - 1;
    const currentMessage = this.messages[botMessageIndex];

    this.generationSubscription = this.apiService.sendMessageStream(userText).subscribe({
      next: (responseChunk) => {
        const textChunk = responseChunk.answer ?? '';
        this.currentBotMessageLength += textChunk.length;
        currentMessage.botData = responseChunk;
        currentMessage.isRated = false;
        this.simulateTypingFragment(textChunk, botMessageIndex);
      },
      error: (err) => {
        this.messages[botMessageIndex].text = "Oops, something went wrong...";
        this.isGenerating = false;
      },
      complete: () => {
        this.isGenerating = false;
        this.generationSubscription?.unsubscribe();
      }
    });
  }

  private saveCompletedBotMessage(messageIndex: number): void {
    if (messageIndex != null) {
      const message = this.messages[messageIndex];

      const responseToSave: AiEngineResponseDetailsDto = {
        id: message.botData?.id!,
        userRequestId: message.botData?.userRequestId!,
        userRequest: message.botData?.userRequest!,
        answer: message.text,
        rating: null,
        createdDateTime: new Date(),
        modifiedDateTime: new Date()
      };

      this.apiService.saveAiEngineResponse(responseToSave).subscribe();
    }
  }

  private simulateTypingFragment(fragment: string, messageIndex: number): void {
    const typingSpeed = 30;

    const fragment$ = timer(0, typingSpeed).pipe(
      take(fragment.length),
      tap(i => {
        this.zone.run(() => {
          if (!this.isCanceled) {
            this.messages[messageIndex].text += fragment[i];
            this.currentGeneratedBotMessageLength += fragment[i].length;
            this.cdr.detectChanges();
          }
        });
      })
    );

    if (!this.isCanceled) {
        this.typingQueue.push(fragment$);
        this.processTypingQueue(messageIndex);
    }
  }

  private processTypingQueue(messageIndex: number): void {
    if (this.typingInProgress || this.typingQueue.length === 0) return;

    this.typingInProgress = true;
    const current = this.typingQueue.shift()!;
    const message = this.messages[messageIndex];

    current.pipe(
      finalize(() => {
        this.typingInProgress = false;
        if (this.typingQueue.length > 0) {
          this.processTypingQueue(messageIndex);
        } else if (!this.isMessageGenerating) {
          this.isGenerating = false;
          this.saveCompletedBotMessage(messageIndex);
        }
      })
    ).subscribe();
  }

  private cancelGeneration(): void {
    if (!this.generationSubscription) return;

    this.isCanceled = true;
    this.isGenerating = false;
    this.resetLengthCounters();
    this.generationSubscription.unsubscribe();
    
    const cancelledMessageId = this.messages.length - 1;
    const partialMessageBody = this.messages[cancelledMessageId];

    if (partialMessageBody.sender === 'bot' && partialMessageBody.botData) {
      this.saveCompletedBotMessage(cancelledMessageId);
    }
  }

  rateMessage(message: ChatMessage, rating: boolean): void {
    if (!message.botData || message.isRated) return;

    message.isRated = true;
    message.botData.rating = rating;

    this.apiService.rateResponse(message.botData.id, rating).subscribe({
      error: err => {
        message.isRated = false;
        message.botData!.rating = null;
      }
    });
  }

  private scrollToBottom(): void {
    try {
      this.chatContainer.nativeElement.scrollTop = this.chatContainer.nativeElement.scrollHeight;
    } catch(err) { }
  }
}