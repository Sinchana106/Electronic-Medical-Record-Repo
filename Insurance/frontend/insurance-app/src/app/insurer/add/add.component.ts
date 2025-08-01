import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card'
import { MatError, MatFormFieldModule } from '@angular/material/form-field';
import { MatRadioModule } from '@angular/material/radio';
import { Insurer } from '../../models/Insurer';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButton } from '@angular/material/button';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { InsurerService } from '../../services/insurer.service';


@Component({
  selector: 'app-add',
  imports: [MatCardModule, FormsModule, MatFormFieldModule, MatInputModule, MatSelectModule, CommonModule, MatError,
    FormsModule,
    ReactiveFormsModule,
    MatRadioModule, MatCheckboxModule, MatButton],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './add.component.html',
  styleUrl: './add.component.css'
})
export class AddComponent {
  newInsurer: Insurer = {
    id: '',
    name: '',
    email: '',
    phone: '',
  }
  counter: number = 0; // Counter to generate unique IDs
  subscription: Subscription | undefined; // Subscription to handle the observable
  errorMessage: string = ''; // Variable to hold error messages
  insurerData!: Insurer;
  insuranceNames: string[] = []; // Array to hold insurance names
  constructor(private insurerService: InsurerService,
    private router: Router) { }
  ngOnInit() {
    this.subscription = this.insurerService.getAllInsurer().subscribe({
      next: (insurer: Insurer[]) => {
        // Get the current count
        this.counter = insurer.length; // Set the counter to the length of the insurer array
        
      },
      error: (err) => {
        console.error('Error loading insurance list:', err);
        this.counter = 0; // Reset counter in case of error
      }
    });
  }
 
  handleSubmit() {
    this.insurerData = this.newInsurer; // Assign the new insurer data to insurerData
    this.insurerData.id = this.generateUniqueID();
    this.subscription = this.insurerService.createInsurer(this.insurerData).subscribe({
      next: (data: any) => {
        console.log(this.insurerData);

        console.log('Insurer added successfully:', this.insurerData);
        alert('Insurer added successfully!'); // Show a success message
        this.router.navigate(['/']); // Navigate to the insurer list after adding a new insurer
      },
      error: (err) => {
        //this.err
        console.error('Error adding insurer:', err);
        alert('Failed to add insurer. Please try again.'); // Show an error message
      }
    });
  }


  generateUniqueID(prefix: string = 'I'): string {
    this.counter++;
    return `${prefix}-${this.counter}`;
  }

  ngOnDestroy() {
    // Cleanup logic if needed
    this.subscription?.unsubscribe(); // Unsubscribe to prevent memory leaks
  }
}
