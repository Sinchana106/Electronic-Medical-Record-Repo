import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { InsurerService} from '../../services/insurer.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-delete',
  imports: [],
  templateUrl: './delete.component.html',
  styleUrl: './delete.component.css'
})
export class DeleteComponent {
  insurerID: string = '';
  subscription: Subscription | undefined; // Subscription to handle the observable
  // This component is used to view the details of a specific patient.
  constructor(public insurerService: InsurerService,
    public route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.insurerID = this.route.snapshot.paramMap.get('id') || '';
    if (this.insurerID) {
      this.subscription = this.insurerService.deleteInsurer(this.insurerID).subscribe({
        next: (data: any) => {
          alert(`Insurer with ID: ${this.insurerID} deleted successfully`);
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
