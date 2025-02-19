import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-patient-filters',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './patient-filters.component.html',
  styleUrls: ['./patient-filters.component.css']
})
export class PatientFiltersComponent {
  @Output() search = new EventEmitter<{ name?: string; fileNo?: number; phoneNumber?: string }>();

  filters = {
    name: '',
    fileNo: null as number | null,
    phoneNumber: ''
  };

  onSearch(): void {
    // Only emit non-empty values
    const filtersToEmit = {
      name: this.filters.name || undefined,
      fileNo: this.filters.fileNo || undefined,
      phoneNumber: this.filters.phoneNumber || undefined
    };

    // Only emit if at least one filter has a value
    if (Object.values(filtersToEmit).some(value => value !== undefined)) {
      this.search.emit(filtersToEmit as { name: string; fileNo: number; phoneNumber: string });
    }
  }

  clearFilters(): void {
    this.filters = {
      name: '',
      fileNo: null,
      phoneNumber: ''
    };
  }
}
