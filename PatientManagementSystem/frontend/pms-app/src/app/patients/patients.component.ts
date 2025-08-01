import { Component, ViewChild } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Patient } from '../models/patient'; // Adjust the import path as necessary
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { PatientService } from '../services/patient.service';
import { RouterLink } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
//const patients: Patient[] = [
//  {
//    id: "64a1f2e8c9a1b2d3e4f5a6b7",
//    name: "John Doe",
//    contactNo: "9876543210",
//    type: "Outpatient",
//    age: 34,
//    date: "2025-07-08",
//    insuredBy: "Blue Cross",
//    visitType: "Consultation",
//    description: "Follow-up for hypertension"
//  },
//  {
//    id: "64a1f2e8c9a1b2d3e4f5a6b8",
//    name: "Jane Smith",
//    contactNo: "9123456780",
//    type: "Inpatient",
//    age: 45,
//    date: "2025-07-07",
//    insuredBy: "Aetna",
//    visitType: "Surgery",
//    description: "Knee replacement surgery"
//  },
//  {
//    id: "64a1f2e8c9a1b2d3e4f5a6b9",
//    name: "Ali Khan",
//    contactNo: "9988776655",
//    type: "Outpatient",
//    age: 29,
//    date: "2025-07-06",
//    insuredBy: "United Health",
//    visitType: "Check-up",
//    description: ""
//  },
//  {
//    id: "64a1f2e8c9a1b2d3e4f5a6ba",
//    name: "Maria Garcia",
//    contactNo: "9001122334",
//    type: "Emergency",
//    age: 52,
//    date: "2025-07-05",
//    insuredBy: "Cigna",
//    visitType: "Emergency",
//    description: "Chest pain and shortness of breath"
//  },
  //{
  //  id: "64a1f2e8c9a1b2d3e4f5a6bb",
  //  name: "Chen Wei",
  //  contactNo: "9112233445",
  //  type: "Outpatient",
  //  age: 38,
  //  date: "2025-07-04",
  //  insuredBy: "Humana",
  //  visitType: "Consultation",
  //  description: "Routine diabetes management"
  //}
//];

@Component({
  selector: 'app-patients',
  imports: [MatCardModule, MatTableModule, MatCheckboxModule,
    MatPaginator, RouterLink, MatIconModule, FormsModule, MatButtonModule],
  templateUrl: './patients.component.html',
  styleUrl: './patients.component.css'
})



export class PatientsComponent {
  // This component can be used to display a list of patients or other related information.
  // You can add properties and methods as needed for your application logic.
  // For example, you might want to fetch patient data from a service and display it in a table.

  displayedColumns: string[] = ['select','id', 'name', 'contactNo', 'type', 'age', 'insuredBy', 'visitType', 'description','actions'];
 
  patients: Patient[] = []; // This will hold the patient data fetched from the service.

  selection = new SelectionModel(true, []);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(private patientService: PatientService) {
    this.patientService.getAllPatients().subscribe((patients: Patient[]) => {
      this.patients = patients;
      this.dataSource = new MatTableDataSource(this.patients);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  dataSource = new MatTableDataSource(this.patients); // This should be populated with patient data, possibly from a service.
  selectHandler(row: Patient) {
    // Toggle selection
    this.selection.toggle(row as never);
  }

}
