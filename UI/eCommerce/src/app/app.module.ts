import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductListComponent } from './feature/product-list/product-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { ProfileComponent } from './feature/profile/profile.component';
import { BucketListComponent } from './feature/bucket-list/bucket-list.component';
import { LoginComponent } from './feature/login/login.component';
import { UserRegistrationComponent } from './feature/user-registration/user-registration.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductListComponent,
    ProfileComponent,
    BucketListComponent,
    LoginComponent,
    UserRegistrationComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    // HttpClientModule,
    AppRoutingModule,
    NgbModalModule,
    ReactiveFormsModule,
  ],
  providers: [
    provideClientHydration(withEventReplay()),
    provideHttpClient(withFetch()),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
