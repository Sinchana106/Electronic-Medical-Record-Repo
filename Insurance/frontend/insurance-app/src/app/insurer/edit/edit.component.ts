import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatError, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButton } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { Insurer } from '../../models/Insurer';
import { InsurerService } from '../../services/insurer.service';
@Component({
  selector: 'app-edit',
  imports: [ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule,
    MatButton, MatError, CommonModule,FormsModule],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.css'
})
export class EditComponent {


  editInsurerForm !: FormGroup;

  editInsuranceData!: Insurer;
  subscription: Subscription | undefined; // Subscription to handle the observable
  
  constructor(private insurerService: InsurerService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder) {
    // Initialize the form with validation
    this.editInsurerForm = this.formBuilder.group({
      id: ['', Validators.required],
      name: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]]
    });
  }
  ngOnInit() {
    // Get the insurer ID from the route parameters
    var insurerId = this.route.snapshot.paramMap.get('id') || '';
    if (insurerId) {
      // Fetch the insurer data using the service
      this.insurerService.getInsurerById(insurerId).subscribe(
        (insurer: Insurer) => {
          console.log(insurer);
          this.editInsuranceData = insurer;
          // Populate the form with existing patient data
          this.editInsurerForm.patchValue({
            id: insurerId,
            name: insurer.name,
            email: insurer.email,
            phone: insurer.phone
          });
        },
        error => {
          console.error('Error fetching insurer data:', error);
        }
      );
    }
  }
  onSubmit() {
    // Handle form submission logic here
    if (this.editInsurerForm.valid && this.editInsuranceData.id != null) {
      this.insurerService.updateInsurer(this.editInsuranceData.id, this.editInsurerForm.value).subscribe({
        next: () => {
          alert('Insurer updated successfully');
          this.router.navigate(['/']); // Navigate back to the insurer list
        },
        error: (error) => {
          console.error('Error updating insurer:', error);
        }
      });
      console.log('Form submitted');
    }
  }
}
