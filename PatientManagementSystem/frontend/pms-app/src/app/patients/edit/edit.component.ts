import { Component } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatError, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Patient } from '../../models/patient';
import { PatientService } from '../../services/patient.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButton } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';
@Component({
  selector: 'app-edit',
  imports: [ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule,
    MatButton, MatError, CommonModule, MatSelectModule, FormsModule,
    MatRadioModule, MatCheckboxModule ],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.css'
})
export class EditComponent {


  editPatientForm !: FormGroup;

  editPatientData!: Patient;
  subscription: Subscription | undefined; // Subscription to handle the observable
  insuranceNames: string[] = []; // Array to hold insurance names

  constructor(private patientService: PatientService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder) {
    // Initialize the form with validation
    this.editPatientForm = this.formBuilder.group({
      id: ['', Validators.required],
      name: ['', [Validators.required, Validators.minLength(2)]],
      contactNo: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      type: ['', Validators.required],
      age: ['', [Validators.required, Validators.min(0),Validators.max(120)]],
      insuredBy: ['', Validators.required],
      visitType: ['', Validators.required],
      description: ['']
    });
  }
  ngOnInit() {
    // Get the patient ID from the route parameters
    var patientId = this.route.snapshot.paramMap.get('id')|| '';
    if (patientId) {
      // Fetch the patient data using the service
      this.patientService.getPatientById(patientId).subscribe(
        (patient: Patient) => {
          console.log(patient);
          this.editPatientData = patient;
          // Populate the form with existing patient data
          this.editPatientForm.patchValue({
            id: patientId,
            name: patient.name,
            contactNo: patient.contactNo,
            type: patient.type,
            age: patient.age,
            insuredBy: patient.insuredBy,
            visitType: patient.visitType,
            description: patient.description
          });
        },
        error => {
          console.error('Error fetching patient data:', error);
        }
      );
    }

    this.subscription = this.patientService.getListOfInsuranceName().subscribe(
      (data) => {
        this.insuranceNames = data; // Assign the fetched insurance names to the array
        console.log('Insurance names fetched:', this.insuranceNames);
      },
      (error) => {
        console.error('Error fetching insurance names:', error);
      }
    );
  }
  onVisitTypeChange(type: string, checked: boolean) {
    const index = this.editPatientData.visitType.indexOf(type);
    if (checked && index === -1) {
      this.editPatientData.visitType.push(type);
    } else if (!checked && index !== -1) {
      this.editPatientData.visitType.splice(index, 1);
    }
  }
  onSubmit() {
    // Handle form submission logic here
    if (this.editPatientForm.valid && this.editPatientData.id!=null) {
      this.patientService.updatePatient(this.editPatientData.id, this.editPatientForm.value).subscribe({
        next: () => {
          alert('Patient updated successfully');
          this.router.navigate(['/']); // Navigate back to the patients list
        },
        error: (error) => {
          console.error('Error updating patient:', error);
        }
      });
      console.log('Form submitted');
    }
  }
}
