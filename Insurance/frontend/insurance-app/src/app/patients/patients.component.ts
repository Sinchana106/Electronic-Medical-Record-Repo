import { Component, ViewChild } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Patient } from '../dtos/patient'; // Adjust the import path as necessary
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { InsurerService } from '../services/insurer.service';

import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-patients',
  imports: [MatCardModule, MatTableModule, MatCheckboxModule,
    MatPaginator, MatIconModule, FormsModule,MatButtonModule],
  templateUrl: './patients.component.html',
  styleUrl: './patients.component.css'
})



export class PatientsComponent {
  // This component can be used to display a list of patients or other related information.
  // You can add properties and methods as needed for your application logic.
  // For example, you might want to fetch patient data from a service and display it in a table.

  displayedColumns: string[] = ['id', 'name', 'contactNo', 'type', 'age', 'insuredBy', 'visitType', 'description'];

  patients: Patient[] = []; // This will hold the patient data fetched from the service.

  selection = new SelectionModel(true, []);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(private insurerService: InsurerService) {
    this.insurerService.getAllPatients().subscribe((patients) => {
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
  refresh() {
    this.insurerService.getAllPatients().subscribe((patients:Patient []) => {
      this.patients = patients;
      this.dataSource = new MatTableDataSource(this.patients);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

}
