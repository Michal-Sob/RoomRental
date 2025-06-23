import {Component, OnInit} from '@angular/core';
import {BuildingListDto, RoomDto, BuildingDto} from '../../models/models';
import {ApiService} from "../../services/api.service";
import {CommonModule} from "@angular/common";
import {MatCardModule} from "@angular/material/card";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatChipsModule} from "@angular/material/chips";
import {MatRippleModule} from "@angular/material/core";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {AddRoomDialogComponent} from "../add-room-dialog/add-room-dialog.component";
import {MatTooltipModule} from "@angular/material/tooltip";

@Component({
  selector: 'app-buildings',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatRippleModule,
    MatTooltipModule
  ],
  templateUrl: './buildings.component.html',
  styleUrl: './buildings.component.scss'
})
export class BuildingsComponent implements OnInit {
  buildings: BuildingListDto[] = [];
  rooms: RoomDto[] = [];
  selectedBuilding: BuildingDto | null = null;

  constructor(private apiService: ApiService,
              private router: Router,
              private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadBuildings();
  }

  loadBuildings(): void {
    this.apiService.getBuildings().subscribe({
      next: (buildings) => {
        this.buildings = buildings;
      },
      error: (error) => {
        console.error('Error loading buildings:', error);
      }
    });
  }

  selectBuilding(building: BuildingDto): void {
    this.selectedBuilding = building;
    this.loadRooms(building.id);
  }

  loadRooms(buildingId: number): void {
    this.apiService.getRoomsByBuilding(buildingId).subscribe({
      next: (rooms) => {
        this.rooms = rooms;
      },
      error: (error) => {
        console.error('Error loading rooms:', error);
      }
    });
  }

  reserveRoom(room: RoomDto): void {
    this.router.navigate(['/new-reservation', room.buildingId, room.id]);
    console.log('Reserve room:', room);
  }

  addRoomToBuilding(building: BuildingListDto): void {
    const dialogRef = this.dialog.open(AddRoomDialogComponent, {
      width: '600px',
      data: { building }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Add new room to local state if this building is selected
        if (this.selectedBuilding && this.selectedBuilding.id === building.id) {
          this.rooms.push(result);
        }
      }
    });
  }

  getBuildingStatusText(status: number): string {
    switch (status) {
      case 0: return 'Open';
      case 1: return 'Closed';
      case 2: return 'Under Construction';
      default: return 'Unknown';
    }
  }

  getRoomTypeText(type: number): string {
    switch (type) {
      case 0: return 'Regular';
      case 1: return 'Lecture';
      case 2: return 'VIP';
      default: return 'Unknown';
    }
  }

  getRoomStatusText(status: number): string {
    switch (status) {
      case 0: return 'Available';
      case 1: return 'Under Maintenance';
      case 2: return 'Reserved';
      default: return 'Unknown';
    }
  }
}
