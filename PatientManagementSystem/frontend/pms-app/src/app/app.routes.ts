import { Routes } from '@angular/router';
import { PatientsComponent } from './patients/patients.component';
import { AddComponent } from './patients/add/add.component';
import { EditComponent } from './patients/edit/edit.component';
import { ViewComponent } from './patients/view/view.component';
import { DeleteComponent } from './patients/delete/delete.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { InsurerComponent } from './insurers/insurers.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './auth.guard';

export const routes: Routes = [
  {
    path: '', component: PatientsComponent,
    canActivate: [AuthGuard] // Ensure the user is authenticated before accessing this route
  },
  { path: 'add', component: AddComponent, canActivate: [AuthGuard] }, //Post
  { path: 'edit/:id', component: EditComponent, canActivate: [AuthGuard] }, //PUT
  { path: 'view/:id', component: ViewComponent, canActivate: [AuthGuard] }, //GetByID
  { path: 'delete/:id', component: DeleteComponent, canActivate: [AuthGuard] }, //DELETE
  { path: 'insurers', component: InsurerComponent, canActivate: [AuthGuard] },
  { path: '**', component: LoginComponent }
];
