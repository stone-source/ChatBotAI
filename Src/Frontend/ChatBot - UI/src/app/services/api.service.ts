import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AiEngineResponseDetailsDto } from '../models/ai-response.dto';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:5176/api/v1';

  constructor(private http: HttpClient) { }

  getHistory(id?: string): Observable<AiEngineResponseDetailsDto[]> {
    const url = id ? `${this.apiUrl}/conversations/get/${id}` : `${this.apiUrl}/conversations/get`;
    return this.http.get<AiEngineResponseDetailsDto[]>(url);
  }

  sendMessage(prompt: string): Observable<AiEngineResponseDetailsDto> {
    return this.http.post<AiEngineResponseDetailsDto>(`${this.apiUrl}/user-requests/completions/send`, `"${prompt}"`, {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  rateResponse(id: string, rating: boolean): Observable<void> {
    const body = {
        objectToSave: {
            id,
            rating
        } };
    return this.http.post<void>(`${this.apiUrl}/ai-engine-response/rate/update`, body);
  }

  limitResponse(id: string, responseDisplayedLength: number): Observable<void> {
    const body = {
        objectToSave: {
            id,
            responseDisplayedLength
        } };
    return this.http.post<void>(`${this.apiUrl}/ai-engine-response/displayed-answer-length/update`, body);
  }
}