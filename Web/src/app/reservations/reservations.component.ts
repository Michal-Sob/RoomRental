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
import {MatSnackBar} from "@angular/material/snack-bar";

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
  cancellingIds = new Set<number>();
  deletingIds = new Set<number>();


  constructor(
    private apiService: ApiService,
    private snackBar: MatSnackBar
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

  viewDetails(reservation: ReservationDto): void {
    console.log('View details for reservation:', reservation);
  }

  cancelReservation(reservation: ReservationDto): void {
    const message = `Cancel "${reservation.room?.name}" on ${this.formatDate(reservation.date)}?`;

    if (!confirm(message)) return;

    this.cancellingIds.add(reservation.id);

    this.apiService.cancelReservation(reservation.id).subscribe({
      next: (response) => {
        this.cancellingIds.delete(reservation.id);
        this.snackBar.open(response.message, 'Close', {
          duration: 3000,
          panelClass: ['success-snackbar']
        });

        // Update local state
        this.updateReservationStatus(reservation.id, ReservationStatus.Cancelled);
      },
      error: (error) => {
        this.cancellingIds.delete(reservation.id);
        const errorMessage = error.error?.message || 'Cannot cancel this reservation';
        this.snackBar.open(errorMessage, 'Close', {
          duration: 4000,
          panelClass: ['error-snackbar']
        });
      }
    });
  }

  private updateReservationStatus(reservationId: number, newStatus: ReservationStatus): void {
    const reservation = this.reservations.find(r => r.id === reservationId);
    if (reservation) {
      reservation.status = newStatus;
      this.categorizeReservations();
    }
  }

  isCancelling(reservationId: number): boolean {
    return this.cancellingIds.has(reservationId);
  }

  canCancel(reservation: ReservationDto): boolean {
    if (reservation.status !== ReservationStatus.Confirmed) {
      return false;
    }

    const dateOnly = reservation.date.split('T')[0];
    const reservationDateTime = new Date(`${dateOnly}T${reservation.startTime}:00`);
    const now = new Date();
    // (1000 * 60 * 60) = milliseconds in an hour
    const hoursUntilReservation = (reservationDateTime.getTime() - now.getTime()) / (1000 * 60 * 60);

    return hoursUntilReservation > 2;
  }

  deleteReservation(reservation: ReservationDto): void {
    const message = `Delete reservation for "${reservation.room?.name}" on ${this.formatDate(reservation.date)}?.`;

    if (!confirm(message)) return;

    this.deletingIds.add(reservation.id);

    this.apiService.deleteReservation(reservation.id).subscribe({
      next: (response) => {
        this.deletingIds.delete(reservation.id);
        this.snackBar.open('Reservation deleted', 'Close', {
          duration: 3000,
          panelClass: ['success-snackbar']
        });

        this.removeReservationFromState(reservation.id);
      },
      error: (error) => {
        this.deletingIds.delete(reservation.id);
        const errorMessage = error.error?.message || 'Cannot delete this reservation';
        this.snackBar.open(errorMessage, 'Close', {
          duration: 3000,
          panelClass: ['error-snackbar']
        });
      }
    });
  }

  isDeleting(reservationId: number): boolean {
    return this.deletingIds.has(reservationId);
  }

  canDelete(reservation: ReservationDto): boolean {
    return reservation.status === ReservationStatus.Cancelled;
  }

  private removeReservationFromState(reservationId: number): void {
    this.reservations = this.reservations.filter(r => r.id !== reservationId);
    this.categorizeReservations();
  }

  bookAgain(reservation: ReservationDto): void {
    console.log('Book again:', reservation);
  }

  getStatusText(status: number): string {
    switch (status) {
      case 0: return 'Pending';
      case 1: return 'Confirmed';
      case 2: return 'Cancelled';
      default: return 'Unknown';
    }
  }
}
