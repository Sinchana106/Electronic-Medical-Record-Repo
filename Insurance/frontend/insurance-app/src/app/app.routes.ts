import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { InsurerComponent } from './insurer/insurer.component';
import { AddComponent } from './insurer/add/add.component';
import { DeleteComponent } from './insurer/delete/delete.component';
import { EditComponent } from './insurer/edit/edit.component';
import { ViewComponent } from './insurer/view/view.component';
import { PatientsComponent } from './patients/patients.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './auth.guard';

export const routes: Routes = [
  { path: '', component: InsurerComponent,canActivate:[AuthGuard] },
  { path: 'add', component: AddComponent, canActivate: [AuthGuard] }, //Post
  { path: 'edit/:id', component: EditComponent,canActivate: [AuthGuard] }, //PUT
  { path: 'view/:id', component: ViewComponent, canActivate: [AuthGuard] }, //GetByID
  { path: 'delete/:id', component: DeleteComponent, canActivate: [AuthGuard] }, //DELETE
  { path: 'patients', component: PatientsComponent, canActivate: [AuthGuard] },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
