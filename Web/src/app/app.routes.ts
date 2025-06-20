import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: '/buildings', pathMatch: 'full' },
  {
    path: 'buildings',
    loadComponent: () => import('./buildings/buildings.component')
      .then(m => m.BuildingsComponent)
  },
  {
    path: 'new-reservation',
    loadComponent: () => import('./new-reservation/new-reservation.component')
      .then(m => m.NewReservationComponent)
  }
];
