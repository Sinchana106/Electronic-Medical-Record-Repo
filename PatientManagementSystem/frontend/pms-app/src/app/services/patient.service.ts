import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Patient } from '../models/patient';
import { Insurer } from '../dtos/Insurer';


@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private apiUrl = 'https://localhost:7057/api/Patient'; // Replace with your actual API base URL

  constructor(private http: HttpClient) { }

  getAllPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>(this.apiUrl);
  }

  getPatientById(id: string): Observable<Patient> {
    return this.http.get<Patient>(`${this.apiUrl}/${id}`);
  }

  createPatient(patient: Patient): Observable<string> {
    return this.http.post<string>(this.apiUrl, patient);
  }

  updatePatient(id: string, patient: Patient): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, patient);
  }

  deletePatient(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  getAllInsurer(): Observable<Insurer[]> { 
    return this.http.get<Insurer[]>(`${this.apiUrl}/insurance`);
  }
  getInsuranceNameById(insurerId: string): Observable<{ insuranceName: string }> {
    return this.http.get<{ insuranceName: string }>(`${this.apiUrl}/insurance/${insurerId}`);
  }

  getListOfInsuranceName(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/insurance/names`);
  }

}
