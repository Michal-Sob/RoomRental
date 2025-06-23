import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {BuildingListDto, RoomDto, RoomType} from '../../models/models';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from "@angular/material/dialog";
import {ApiService} from "../../services/api.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatIconModule} from "@angular/material/icon";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {MatButtonModule} from "@angular/material/button";
import {NgForOf, NgIf} from "@angular/common";


@Component({
  selector: 'app-add-room-dialog',
  standalone: true,
  imports: [
    MatDialogContent,
    ReactiveFormsModule,
    MatIconModule,
    MatDialogTitle,
    MatInputModule,
    MatSelectModule,
    MatDialogActions,
    MatButtonModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './add-room-dialog.component.html',
  styleUrl: './add-room-dialog.component.scss'
})
export class AddRoomDialogComponent implements OnInit {
  roomForm: FormGroup;
  availableFloors: number[] = [];
  isLoading = false;
  RoomType = RoomType;

  constructor(
    public dialogRef: MatDialogRef<AddRoomDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { building: BuildingListDto },
    private fb: FormBuilder,
    private apiService: ApiService,
    private snackBar: MatSnackBar
  ) {
    this.roomForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      capacity: [10, [Validators.required, Validators.min(1)]],
      floor: [0, Validators.required],
      type: [RoomType.Regular, Validators.required]
    });
  }

  ngOnInit(): void {
    // Generate available floors (0 to numberOfFloors)
    this.availableFloors = Array.from(
      { length: this.data.building.numberOfFloors + 1 },
      (_, i) => i
    );
    console.log(this.availableFloors);
  }

  getHourlyRate(type: RoomType): number {
    switch (type) {
      case RoomType.Regular: return 50;
      case RoomType.Lecture: return 75;
      case RoomType.VIP: return 150;
      default: return 50;
    }
  }

  onSave(): void {
    if (this.roomForm.valid) {
      this.isLoading = true;

      const formValue = this.roomForm.value;
      const createRoomDto: RoomDto = {
        id: 0, // Default value for id
        status: 0, // Default value for status
        name: formValue.name.trim(),
        capacity: formValue.capacity,
        floor: formValue.floor,
        type: formValue.type,
        buildingId: this.data.building.id
      };

      this.apiService.createRoom(createRoomDto).subscribe({
        next: (newRoom) => {
          this.isLoading = false;
          this.snackBar.open('Room created successfully!', 'Close', {
            duration: 3000,
            panelClass: ['success-snackbar']
          });
          this.dialogRef.close(newRoom);
        },
        error: (error) => {
          this.isLoading = false;
          const errorMessage = error.error?.message || 'Error creating room';
          this.snackBar.open(errorMessage, 'Close', {
            duration: 4000,
            panelClass: ['error-snackbar']
          });
        }
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
