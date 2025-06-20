import { bootstrapApplication } from '@angular/platform-browser';
import { Routes } from '@angular/router';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient } from '@angular/common/http';
import { BuildingsComponent } from './app/buildings/buildings.component';
import { NewReservationComponent } from "./app/new-reservation/new-reservation.component";
import { ReservationsComponent } from "./app/reservations/reservations.component";

const routes: Routes = [
  { path: '', redirectTo: '/buildings', pathMatch: 'full' as const },
  { path: 'buildings', component: BuildingsComponent },
  { path: 'new-reservation', component: NewReservationComponent },
  { path: 'reservations', component: ReservationsComponent },
  { path: '**', redirectTo: '/buildings' }
];

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideAnimations(),
    provideHttpClient()
  ]
});
