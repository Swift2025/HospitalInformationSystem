import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PatientService } from '../../services/patient.service';
import { Patient } from '../../models/patient.model';
import { PatientFiltersComponent } from '../patient-filters/patient-filters.component';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  imports: [CommonModule, RouterModule, PatientFiltersComponent],
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit {
  patients: Patient[] = [];
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  loading = false;
  error = '';
  filters: { name?: string; fileNo?: number; phoneNumber?: string } = {};

  constructor(private patientService: PatientService) { }

  ngOnInit(): void {
    this.loadPatients();
  }

  loadPatients(): void {
    this.loading = true;
    this.error = '';

    this.patientService.getPatients(this.currentPage, this.pageSize, this.filters).subscribe({
      next: (response) => {
        this.patients = response.items;
        this.totalItems = response.totalCount;
        this.loading = false;
      },
      error: (err) => {
        this.error = `Error loading patients: ${err.message}`;
        this.loading = false;
      }
    });
  }

  deletePatient(id: string): void {
    if (!confirm('Are you sure you want to delete this patient?')) {
      return;
    }

    this.loading = true;
    this.error = '';

    this.patientService.deletePatient(id).subscribe({
      next: () => {
        this.loadPatients(); // Refresh the list after deletion
      },
      error: (err) => {
        this.error = `Error deleting patient: ${err.message}`;
        this.loading = false;
      }
    });
  }

  getPages(): number[] {
    const pageCount = Math.ceil(this.totalItems / this.pageSize);
    return Array.from({ length: pageCount }, (_, i) => i + 1);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadPatients();
  }

  onFiltersChange(filters: { name?: string; fileNo?: number; phoneNumber?: string }): void {
    this.filters = filters;
    this.currentPage = 1; // Reset to first page when filters change
    this.loadPatients();
  }
}
