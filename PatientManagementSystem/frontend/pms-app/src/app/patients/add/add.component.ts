import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card'

import { MatError, MatFormFieldModule } from '@angular/material/form-field';
import { MatRadioModule } from '@angular/material/radio';
import { Patient } from '../../models/patient';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatNativeDateModule, provideNativeDateAdapter } from '@angular/material/core';

import { MatButton } from '@angular/material/button';
import { PatientService } from '../../services/patient.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-add',
  imports: [MatCardModule, FormsModule, MatFormFieldModule, MatInputModule, MatSelectModule, CommonModule, MatError,
    FormsModule,
    ReactiveFormsModule,
    MatRadioModule,MatCheckboxModule,MatButton],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [provideNativeDateAdapter()],
  templateUrl: './add.component.html',
  styleUrl: './add.component.css'
})
export class AddComponent {
  newPatient: Patient = {
    id:'',
    name: '',
    contactNo: '',
    type: '',
    age: 1,
    insuredBy: '',
    visitType: ['Consultation'],
    description: ''
  }
  counter: number = 0; // Counter to generate unique IDs
  subscription: Subscription | undefined; // Subscription to handle the observable
  errorMessage: string = ''; // Variable to hold error messages
  patientData!: Patient;
  insuranceNames: string[] = []; // Array to hold insurance names
  constructor(private patientService: PatientService,
    private router: Router) { }
  ngOnInit() {
    this.subscription=this.patientService.getListOfInsuranceName().subscribe(
      (data) => {
        this.insuranceNames = data; // Assign the fetched insurance names to the array
        console.log('Insurance names fetched:', this.insuranceNames);
      },
      (error) => {
        console.error('Error fetching insurance names:', error);
      }
    );
    this.subscription = this.patientService.getAllPatients().subscribe(
      (patients: Patient[]) => {
        this.counter = patients.length; // Set the counter to the current number of patients
        console.log('Current patient count:', this.counter);
      },
      (error) => {
        console.error('Error fetching patients:', error);
        this.counter = 0; // Reset counter in case of error
      }
    ) // Initialize the counter with the current number of patients

    //this.insuranceNames = ['Star Health', 'Max Bupa', 'HDFC ERGO', 'ICICI Lombard', 'Religare Health Insurance', 'United Health', 'Cigna', 'Humana'];
  }
  handleSubmit() {
    this.patientData = this.newPatient; // Assign the new patient data to patientData
    this.patientData.id = this.generateUniqueID();
   // this.patientData.date = (this.newPatient.date).toString(); 
    this.subscription = this.patientService.createPatient(this.patientData).subscribe({
      next: (data: any) => {
        console.log(this.patientData);
         
        console.log('Patient added successfully:', this.patientData);
        alert('Patient added successfully!'); // Show a success message
        this.router.navigate(['/']); // Navigate to the patients list after adding a new patient
      },
      error: (err) => {
        //this.err
        console.error('Error adding patient:', err);
        alert('Failed to add patient. Please try again.'); // Show an error message
      }
    });
  }
  idExists: boolean = false
 // checkDuplicatePatientID() {
 //   // Check if the patient ID already exists
 //// Reset the flag before checking
 //   if (!this.newPatient.id) {
 //     this.idExists = false;
 //     return;
 //   }
 //   this.subscription = this.patientService.getPatientById(this.newPatient.id).subscribe({
 //     next: (data: Patient) => {
 //       if (data && data.id) {
 //         this.idExists = true; // Set the flag to true if ID exists
 //         alert(`Patient ID ${this.newPatient.id} already exists. Please use a different ID.`);
 //         this.newPatient.id = ''; // Clear the ID field
 //       }
 //     },
 //     error: (err) => {
 //       console.error('Error checking patient ID:', err);
 //       this.idExists = false;
 //     }
 //   });
  // }

  generateUniqueID(prefix: string = 'P'): string {
    this.counter++;
    return `${prefix}-${this.counter}`;
  }


  onVisitTypeChange(type: string, checked: boolean) {
    const index = this.newPatient.visitType.indexOf(type);
    if (checked && index === -1) {
      this.newPatient.visitType.push(type);
    } else if (!checked && index !== -1) {
      this.newPatient.visitType.splice(index, 1);
    }
  }


  visitTypeValidator(): { [key: string]: boolean } | null {
    const visitTypes = this.newPatient.visitType;
    if (visitTypes.length === 0) {
      return { required: true };
    }
    return null;
  }



  ngOnDestroy() {
    // Cleanup logic if needed
    this.subscription?.unsubscribe(); // Unsubscribe to prevent memory leaks
  }
}
