import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Insurer } from '../../models/Insurer';
import { InsurerService } from '../../services/insurer.service';
@Component({
  selector: 'app-view',
  imports: [MatCardModule, CommonModule],
  templateUrl: './view.component.html',
  styleUrl: './view.component.css'
})
export class ViewComponent {
  insurerDetails !: Insurer;
  insurerID: string = '';
  subscription: Subscription | undefined; // Subscription to handle the observable
  // This component is used to view the details of a specific patient.
  constructor(public insurerService: InsurerService,
    public route: ActivatedRoute) { }

  ngOnInit(): void {
    this.insurerID = this.route.snapshot.paramMap.get('id') || '';
    if (this.insurerID) {
      this.subscription = this.insurerService.getInsurerById(this.insurerID).subscribe({
        next: (data: Insurer) => this.insurerDetails = data,
        error: (err) => console.error('Error fetching Insurer:', err)
      });
    }
  }
  ngOnDestroy() {
    // Cleanup logic if needed
    this.subscription?.unsubscribe(); // Unsubscribe to prevent memory leaks
  }


}
