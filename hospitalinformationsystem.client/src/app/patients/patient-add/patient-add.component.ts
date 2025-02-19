import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { PatientService } from '../../services/patient.service';
import { Patient } from '../../models/patient.model';

@Component({
  selector: 'app-patient-add',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './patient-add.component.html',
  styleUrls: ['./patient-add.component.css']
})
export class PatientAddComponent {
  patient: Omit<Patient, 'id' | 'recordCreationDate'> = {
    name: '',
    fileNo: 0,
    citizenId: '',
    birthdate: new Date(),
    gender: 0,
    nationality: '',
    phoneNumber: '',
    email: '',
    country: '',
    city: '',
    street: '',
    address1: '',
    address2: '',
    contactPerson: '',
    contactRelation: '',
    contactPhone: '',
    firstVisitDate: new Date()
  };

  loading = false;
  error = '';

  constructor(private patientService: PatientService, private router: Router) { }

  onSubmit(): void {
    this.loading = true;
    this.error = '';

    this.patientService.addPatient(this.patient).subscribe({
      next: () => {
        this.router.navigate(['/patients']);
      },
      error: (err) => {
        this.error = 'Error adding patient: ' + err.message;
        this.loading = false;
      }
    });
  }

  onCancel() {
    this.router.navigate(['/patients']);
  }
}
