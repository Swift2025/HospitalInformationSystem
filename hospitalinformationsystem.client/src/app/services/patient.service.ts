import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Patient } from '../models/patient.model';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private apiUrl = `http://localhost:7169/patients`;

  constructor(private http: HttpClient) { }

  // Get a list of patients with pagination and filters
  getPatients(
    page: number, 
    pageSize: number, 
    filters?: { name?: string; fileNo?: number; phoneNumber?: string }
  ): Observable<{ items: Patient[]; totalCount: number }> {
    let params = new HttpParams()
      .set('pageNumber', page.toString())
      .set('pageSize', pageSize.toString());

    // Add filters to params if they exist
    Object.entries(filters || {}).forEach(([key, value]) => {
      if (value !== undefined && value !== null) {
        params = params.set(key, value.toString());
      }
    });

    return this.http.get<{ items: Patient[]; totalCount: number }>(
      this.apiUrl, 
      { params }
    );
  }

  // Get a single patient by ID
  getPatient(id: string): Observable<Patient> {
    if (!id) {
      throw new Error('Patient ID is required');
    }
    return this.http.get<Patient>(`${this.apiUrl}/${id}`);
  }

  // Add a new patient
  addPatient(patient: Omit<Patient, 'id' | 'recordCreationDate'>): Observable<Patient> {
    if (!patient) {
      throw new Error('Patient data is required');
    }
    return this.http.post<Patient>(this.apiUrl, patient);
  }

  // Update an existing patient
  updatePatient(patient: Patient): Observable<Patient> {
    if (!patient || !patient.id) {
      throw new Error('Patient and patient ID are required');
    }
    return this.http.put<Patient>(`${this.apiUrl}/${patient.id}`, patient);
  }

  // Delete a patient by ID
  deletePatient(id: string): Observable<void> {
    if (!id) {
      throw new Error('Patient ID is required');
    }
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
