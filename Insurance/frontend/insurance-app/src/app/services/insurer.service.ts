import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Insurer } from '../models/Insurer';
import { Observable } from 'rxjs';
import { Patient } from '../dtos/patient';
@Injectable({
  providedIn: 'root'
})
export class InsurerService {
  private apiUrl = 'https://localhost:7180/Insurance'; // Replace with your actual API base URL
  insurer!: Insurer
  constructor(private http: HttpClient) { }

  getAllInsurer(): Observable<Insurer[]> {
    return this.http.get<Insurer[]>(this.apiUrl);
  }
  getAllPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${this.apiUrl}/patients`);
  }
  getInsurerById(id: string): Observable<Insurer> {
    return this.http.get<Insurer>(`${this.apiUrl}/${id}`);
  }

  createInsurer(insurer: Insurer): Observable<string> {
    return this.http.post<string>(this.apiUrl, insurer);
  }

  updateInsurer(id: string, insurer: Insurer): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, insurer);
  }

  deleteInsurer(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }


}
