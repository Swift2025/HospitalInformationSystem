import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { PatientService } from '../../services/patient.service';
import { Patient } from '../../models/patient.model';

@Component({
  selector: 'app-edit-patient',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './edit-patient.component.html',
  styleUrls: ['./edit-patient.component.css']
})
export class EditPatientComponent implements OnInit {
  patient: Patient = {} as Patient;
  loading = false;
  error = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private patientService: PatientService
  ) { }

  ngOnInit(): void {
    this.loadPatient();
  }

  private loadPatient(): void {
    this.loading = true;
    this.error = '';
    
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      this.error = 'No patient ID provided';
      this.loading = false;
      this.router.navigate(['/patients']);
      return;
    }

    this.patientService.getPatient(id).subscribe({
      next: (patient) => {
        this.patient = patient;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error loading patient: ' + err.message;
        this.loading = false;
        this.router.navigate(['/patients']);
      }
    });
  }

  onSubmit(): void {
    if (!this.patient?.id) {
      this.error = 'No patient data to update';
      return;
    }

    if (!this.validatePatient()) {
      return;
    }

    this.loading = true;
    this.error = '';

    this.patientService.updatePatient(this.patient).subscribe({
      next: () => {
        this.router.navigate(['/patients']);
      },
      error: (err) => {
        this.error = 'Error updating patient: ' + err.message;
        this.loading = false;
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/patients']);
  }

  private validatePatient(): boolean {
    if (!this.patient.name?.trim()) {
      this.error = 'Patient name is required';
      return false;
    }
    if (!this.patient.fileNo) {
      this.error = 'File number is required';
      return false;
    }
    if (!this.patient.phoneNumber?.trim()) {
      this.error = 'Phone number is required';
      return false;
    }
    return true;
  }
}
