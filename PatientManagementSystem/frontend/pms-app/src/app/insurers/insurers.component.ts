import { Component, ViewChild } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Insurer } from '../dtos/Insurer';
import { PatientService } from '../services/patient.service';
import {  MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-insurer',
  imports: [MatCardModule, MatTableModule, MatCheckboxModule,
    MatPaginator, MatIconModule, FormsModule,MatButtonModule],
  templateUrl: './insurers.component.html',
  styleUrl: './insurers.component.css'
})
export class InsurerComponent {

  displayedColumns: string[] = ['id', 'name', 'email', 'phone'];

  insurer: Insurer[] = []; // This will hold the patient data fetched from the service.

  selection = new SelectionModel(true, []);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(private patientService: PatientService) {
    this.patientService.getAllInsurer().subscribe((insurer: Insurer[]) => {
      this.insurer = insurer;
      this.dataSource = new MatTableDataSource(this.insurer);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  dataSource = new MatTableDataSource(this.insurer); // This should be populated with patient data, possibly from a service.
  selectHandler(row: Insurer) {
    // Toggle selection
    this.selection.toggle(row as never);
  }
  refresh() {
    this.patientService.getAllInsurer().subscribe((insurer: Insurer[]) => {
      this.insurer = insurer;
      this.dataSource = new MatTableDataSource(this.insurer);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

}

