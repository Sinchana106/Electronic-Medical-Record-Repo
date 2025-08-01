import { Component, ViewChild } from '@angular/core';

import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { InsurerService } from '../services/insurer.service';
import { Insurer } from '../models/Insurer';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-insurer',
  imports: [MatCardModule, MatTableModule, MatCheckboxModule,
    MatPaginator, RouterLink, MatIconModule, FormsModule],
  templateUrl: './insurer.component.html',
  styleUrl: './insurer.component.css'
})
export class InsurerComponent { 

  displayedColumns: string[] = ['select','id', 'name', 'email', 'phone',  'actions'];

  insurer: Insurer[] = []; // This will hold the patient data fetched from the service.

  selection = new SelectionModel(true, []);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(private insuranceService: InsurerService) {
    this.insuranceService.getAllInsurer().subscribe((insurer: Insurer[]) => {
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

}

