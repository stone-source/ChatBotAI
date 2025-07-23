import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AiEngineResponseDetailsDto } from '../models/ai-response.dto';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost:7181/api/v1';

  constructor(private http: HttpClient) { }

  saveAiEngineResponse(response: AiEngineResponseDetailsDto): Observable<void> {
    const url = `${this.apiUrl}/ai-engine-response/save`;
    return this.http.post<void>(url, response);
  }

  rateResponse(id: string, rating: boolean): Observable<void> {
    const body = {
        objectToSave: {
            id,
            rating
        } };
    return this.http.post<void>(`${this.apiUrl}/ai-engine-response/rate/update`, body);
  }

  getHistory(id?: string): Observable<AiEngineResponseDetailsDto[]> {
    const url = id ? `${this.apiUrl}/conversations/get/${id}` : `${this.apiUrl}/conversations/get`;
    return this.http.get<AiEngineResponseDetailsDto[]>(url);
  }

  sendMessageStream(message: string): Observable<AiEngineResponseDetailsDto> {
    return new Observable(observer => {
      const eventSource = new EventSource(`${this.apiUrl}/user-requests/completions/send?request=${message}`);

      eventSource.onmessage = (event) => {
        try {
          const lines = event.data.split('\n');
          lines.forEach((line: string) => {
            if (line.trim()) {
              try {
                if (line.trim() === '[DONE]') {
                  observer.complete();
                  eventSource.close();
                  return;
                }

                const parsedData: AiEngineResponseDetailsDto = JSON.parse(line, this.camelCaseReviver);
                observer.next(parsedData);
              } catch (parseErr) {
                console.warn('Unable to parse JSON fragment:', line, parseErr);
              }
            }
          });
        } catch (err) {
          console.error('Error processing EventSource data:', err);
        }
      };

      return () => {
        eventSource.close();
      };
    });
  }

  camelCaseReviver(key: string, value: any) {
    if (
      value !== null &&
      typeof value === 'object' &&
      !Array.isArray(value)
    ) {
      const newObj: any = {};
      for (const prop in value) {
        if (value.hasOwnProperty(prop)) {
          const camelKey = prop.charAt(0).toLowerCase() + prop.slice(1);
          newObj[camelKey] = value[prop];
        }
      }
      return newObj;
    }
    return value;
  }
}