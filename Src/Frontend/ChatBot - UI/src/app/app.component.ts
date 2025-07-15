import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewChecked } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from './services/api.service';
import { AiEngineResponseDetailsDto } from './models/ai-response.dto';
import { Subscription, timer, take, tap, finalize } from 'rxjs';
import { CommonModule, NgClass, NgFor, NgIf } from '@angular/common';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

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

  private generationSubscription: Subscription | null = null;
  private currentBotMessageId: string | null = null;

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
  this.isLoading = true;

  this.apiService.getHistory('2A89FCF3-27A9-481F-8437-007212BFA61E').subscribe({
    next: (data) => {
      this.messages = [];

      for (const item of data) {
        this.messages.push({
          sender: 'user',
          text: item.userRequest.request,
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
    if (this.isGenerating) {
      this.cancelGeneration();
    } else {
      this.sendMessage();
    }
  }

  private sendMessage(): void {
    if (this.userInput.invalid) return;

    const userText = this.userInput.value!;
    this.messages.push({ sender: 'user', text: userText });
    this.userInput.reset();
    this.isGenerating = true;

    const botMessageIndex = this.messages.push({ sender: 'bot', text: '' }) - 1;

    this.apiService.sendMessage(userText).subscribe({
      next: (response) => {
        this.messages[botMessageIndex].botData = response;
        this.currentBotMessageId = response.id;
        this.simulateTyping(response.answer || '', botMessageIndex);
      },
      error: (err) => {
        this.messages[botMessageIndex].text = "Oops, something went wrong...";
        this.isGenerating = false;
      }
    });
  }

  private simulateTyping(fullText: string, messageIndex: number): void {
    const typingSpeed = 30;
    this.generationSubscription = timer(0, typingSpeed).pipe(
      take(fullText.length),
      tap(i => this.messages[messageIndex].text += fullText[i]),
      finalize(() => {
        this.isGenerating = false;
        this.currentBotMessageId = null;
        this.generationSubscription = null;
      })
    ).subscribe();
  }

  private cancelGeneration(): void {
    if (!this.generationSubscription) return;

    const currentMessageId = this.currentBotMessageId;

    this.generationSubscription.unsubscribe();

    const partialMessage = this.messages[this.messages.length - 1];
    const generatedLength = partialMessage.text.length;

    this.apiService.limitResponse(currentMessageId!, generatedLength).subscribe({});

    this.isGenerating = false;
    this.currentBotMessageId = null;
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
