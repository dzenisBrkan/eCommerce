import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductListComponent } from './feature/product-list/product-list.component';
import { ProfileComponent } from './feature/profile/profile.component';
import { BucketListComponent } from './feature/bucket-list/bucket-list.component';
import { LoginComponent } from './feature/login/login.component';
import { UserRegistrationComponent } from './feature/user-registration/user-registration.component';
import { AuthGuard } from './feature/guards/auth.guard';

const routes: Routes = [
  { path: 'products', component: ProductListComponent },
  { path: 'bucket', component: BucketListComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'user-registration', component: UserRegistrationComponent },
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  { path: '**', redirectTo: '/products' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
