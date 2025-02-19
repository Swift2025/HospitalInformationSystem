import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PatientListComponent } from '../app/patients/patient-list/patient-list.component';
import { PatientAddComponent } from '../app/patients/patient-add/patient-add.component';
import { EditPatientComponent } from '../app/patients/edit-patient/edit-patient.component';

export const routes: Routes = [
  { path: 'patients', component: PatientListComponent },
  { path: 'add-patient', component: PatientAddComponent },
  { path: 'edit-patient/:id', component: EditPatientComponent },
  { path: '', redirectTo: '/patients', pathMatch: 'full' }, // Default route
  { path: '**', redirectTo: '/patients' } // Fallback route for unknown paths
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
