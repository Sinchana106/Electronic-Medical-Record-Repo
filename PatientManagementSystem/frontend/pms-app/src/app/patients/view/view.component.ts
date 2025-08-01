import { Component } from '@angular/core';
import { Patient } from '../../models/patient'; // Adjust the import path as necessary}
import { PatientService } from '../../services/patient.service'; // Adjust the import path as necessary
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-view',
  imports: [MatCardModule, CommonModule],
  templateUrl: './view.component.html',
  styleUrl: './view.component.css'
})
export class ViewComponent {
  patientDetails !: Patient;
  patientID: string = '';
  subscription: Subscription | undefined; // Subscription to handle the observable
  // This component is used to view the details of a specific patient.
  constructor(public patientService: PatientService,
    public route: ActivatedRoute) { }

  ngOnInit(): void {
    this.patientID = this.route.snapshot.paramMap.get('id') || '';
    if (this.patientID) {
      this.subscription=this.patientService.getPatientById(this.patientID).subscribe({
        next: (data: Patient) => this.patientDetails = data,
        error: (err) => console.error('Error fetching patient:', err)
      });
    }
  }
  ngOnDestroy() {
    // Cleanup logic if needed
    this.subscription?.unsubscribe(); // Unsubscribe to prevent memory leaks
  }
 

}
