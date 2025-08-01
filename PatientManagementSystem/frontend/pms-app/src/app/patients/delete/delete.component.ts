import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientService } from '../../services/patient.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-delete',
  imports: [],
  templateUrl: './delete.component.html',
  styleUrl: './delete.component.css'
})
export class DeleteComponent {
  patientID: string = '';
  subscription: Subscription | undefined; // Subscription to handle the observable
  // This component is used to view the details of a specific patient.
  constructor(public patientService: PatientService,
    public route: ActivatedRoute,
  private router: Router) { }

  ngOnInit(): void {
    this.patientID = this.route.snapshot.paramMap.get('id') || '';
    if (this.patientID) {
      this.subscription=this.patientService.deletePatient(this.patientID).subscribe({
        next: (data: any) => {
          alert(`Patient with ID: ${this.patientID} deleted successfully`);
          this.router.navigate(['/']);
        },
        error: (err) => {
          console.error('Error deleting patient:', err);
        }
      });
    }
  }
  ngOnDestroy() {
    // Cleanup logic if needed
    this.subscription?.unsubscribe(); // Unsubscribe to prevent memory leaks
  }


}
