import {Component, OnInit} from '@angular/core';
import {ReservationDto, ReservationListDto, ReservationStatus} from "../../models/models";
import {ApiService} from "../../services/api.service";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {MatCardModule} from "@angular/material/card";
import {MatChipsModule} from "@angular/material/chips";
import {MatTabsModule} from "@angular/material/tabs";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {RouterLink} from "@angular/router";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-reservations',
  standalone: true,
  imports: [
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    MatChipsModule,
    MatTabsModule,
    MatProgressSpinnerModule,
    RouterLink,
    NgIf,
    NgForOf
  ],
  templateUrl: './reservations.component.html',
  styleUrl: './reservations.component.scss'
})
export class ReservationsComponent implements OnInit {
  reservations: ReservationDto[] = [];
  upcomingReservations: ReservationDto[] = [];
  pastReservations: ReservationDto[] = [];
  cancelledReservations: ReservationDto[] = [];
  isLoading = true;

  constructor(
    private apiService: ApiService,
  ) {}

  ngOnInit(): void {
    this.loadReservations();
  }

  loadReservations(): void {
    this.isLoading = true;

    this.apiService.getReservations().subscribe({
      next: (reservations) => {
        this.reservations = reservations;
        this.categorizeReservations();
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading reservations:', error);
        this.isLoading = false;
      }
    });
  }

  categorizeReservations(): void {
    const now = new Date();
    const today = new Date(now.getFullYear(), now.getMonth(), now.getDate());

    this.upcomingReservations = this.reservations.filter(r => {
      const reservationDate = new Date(r.date);
      return reservationDate >= today && r.status !== ReservationStatus.Cancelled;
    });

    this.pastReservations = this.reservations.filter(r => {
      const reservationDate = new Date(r.date);
      return reservationDate < today && r.status !== ReservationStatus.Cancelled;
    });

    this.cancelledReservations = this.reservations.filter(r =>
      r.status === ReservationStatus.Cancelled
    );
  }

  onTabChange(event: any): void {
    // Handle tab change if needed
  }

  isToday(date: string): boolean {
    const today = new Date();
    const reservationDate = new Date(date);
    return today.toDateString() === reservationDate.toDateString();
  }

  formatDate(date: string): string {
    const reservationDate = new Date(date);
    const today = new Date();
    const tomorrow = new Date(today);
    tomorrow.setDate(tomorrow.getDate() + 1);

    if (reservationDate.toDateString() === today.toDateString()) {
      return 'Today';
    } else if (reservationDate.toDateString() === tomorrow.toDateString()) {
      return 'Tomorrow';
    } else {
      return reservationDate.toLocaleDateString('en-US', {
        weekday: 'short',
        month: 'short',
        day: 'numeric',
        year: 'numeric'
      });
    }
  }

  getStatusText(status: number): string {
    switch (status) {
      case 0: return 'Pending';
      case 1: return 'Confirmed';
      case 2: return 'Cancelled';
      default: return 'Unknown';
    }
  }

  canCancel(reservation: ReservationDto): boolean {
    const reservationDate = new Date(reservation.date);
    const now = new Date();
    const hoursUntilReservation = (reservationDate.getTime() - now.getTime()) / (1000 * 60 * 60);

    return hoursUntilReservation > 2 && reservation.status === ReservationStatus.Confirmed;
  }

  viewDetails(reservation: ReservationDto): void {
    // Implement view details functionality
    console.log('View details for reservation:', reservation);
  }

  cancelReservation(reservation: ReservationDto): void {
    // Implement cancel reservation functionality
    if (confirm('Are you sure you want to cancel this reservation?')) {
      // API call to cancel reservation
      console.log('Cancel reservation:', reservation);
    }
  }

  bookAgain(reservation: ReservationDto): void {
    // Navigate to new reservation form with pre-filled data
    console.log('Book again:', reservation);
  }
}
