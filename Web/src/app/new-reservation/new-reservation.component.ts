import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {BuildingDto, CreateReservationDto, RoomDto, RoomType, BuildingListDto} from "../../models/models";
import {ApiService} from '../../services/api.service';
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatCardModule} from "@angular/material/card";
import {MatIconModule} from "@angular/material/icon";
import {MatSelectModule} from "@angular/material/select";
import {MatDatepickerModule} from "@angular/material/datepicker";
import { MatNativeDateModule } from '@angular/material/core';
import {MatButtonModule} from "@angular/material/button";
import {MatInputModule} from "@angular/material/input";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-new-reservation',
  standalone: true,
  imports: [
    MatCardModule,
    MatIconModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatButtonModule,
    MatInputModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './new-reservation.component.html',
  styleUrl: './new-reservation.component.scss'
})
export class NewReservationComponent implements OnInit {
  reservationForm: FormGroup;
  buildings: BuildingListDto[] = [];
  availableRooms: RoomDto[] = [];
  selectedBuilding: BuildingDto | null = null;
  selectedRoom: RoomDto | null = null;
  isLoading = false;
  minDate = new Date();
  RoomType = RoomType;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private snackBar: MatSnackBar
  ) {
    this.reservationForm = this.fb.group({
      buildingId: ['', Validators.required],
      roomId: ['', Validators.required],
      date: ['', Validators.required],
      startTime: ['', Validators.required],
      endTime: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadBuildings();
    this.setupFormSubscriptions();
  }

  loadBuildings(): void {
    this.apiService.getBuildings().subscribe({
      next: (buildings) => {
        this.buildings = buildings;
      },
      error: (error) => {
        console.error('Error loading buildings:', error);
        this.snackBar.open('Error loading buildings', 'Close', {duration: 3000});
      }
    });
  }

  setupFormSubscriptions(): void {
    this.reservationForm.get('roomId')?.valueChanges.subscribe(roomId => {
      this.selectedRoom = this.availableRooms.find(r => r.id === roomId) || null;
    });
  }

  onBuildingChange(buildingId: number): void {
    this.selectedBuilding = this.buildings.find(b => b.id === buildingId) || null;
    this.reservationForm.patchValue({roomId: ''});
    this.selectedRoom = null;

    if (buildingId) {
      this.loadRooms(buildingId);
    }
  }

  loadRooms(buildingId: number): void {
    this.apiService.getRoomsByBuilding(buildingId).subscribe({
      next: (rooms) => {
        this.availableRooms = rooms;
      },
      error: (error) => {
        console.error('Error loading rooms:', error);
        this.snackBar.open('Error loading rooms', 'Close', {duration: 3000});
      }
    });
  }

  onSubmit(): void {
    if (this.reservationForm.valid) {
      this.isLoading = true;

      const formValue = this.reservationForm.value;
      const reservation: CreateReservationDto = {
        date: formValue.date.toISOString().split('T')[0],
        startTime: formValue.startTime,
        endTime: formValue.endTime,
        userId: 1, // Mock user ID
        roomId: formValue.roomId
      };

      this.apiService.createReservation(reservation).subscribe({
        next: (result) => {
          this.isLoading = false;
          this.snackBar.open('Reservation created successfully!', 'Close', {
            duration: 5000,
            panelClass: ['success-snackbar']
          });
          this.resetForm();
        },
        error: (error) => {
          this.isLoading = false;
          const errorMessage = error.error?.message || error.message || 'Error creating reservation';
          this.snackBar.open(errorMessage, 'Close', {
            duration: 5000,
            panelClass: ['error-snackbar']
          });
        }
      });
    }
  }
  resetForm(): void {
    this.reservationForm.reset();
    this.selectedBuilding = null;
    this.selectedRoom = null;
    this.availableRooms = [];
  }
}
