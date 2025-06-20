import {Component, OnInit} from '@angular/core';
import {Building, Room} from '../../models/models';
import {ApiService} from "../../services/api.service";
import {CommonModule} from "@angular/common";
import {MatCardModule} from "@angular/material/card";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatChipsModule} from "@angular/material/chips";
import {MatRippleModule} from "@angular/material/core";

@Component({
  selector: 'app-buildings',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatRippleModule
  ],
  templateUrl: './buildings.component.html',
  styleUrl: './buildings.component.scss'
})
export class BuildingsComponent implements OnInit {
  buildings: Building[] = [];
  rooms: Room[] = [];
  selectedBuilding: Building | null = null;

  constructor(private apiService: ApiService) {}

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

  selectBuilding(building: Building): void {
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

  reserveRoom(room: Room): void {
    // Navigate to reservation form with selected room
    // This will be implemented in the routing
    console.log('Reserve room:', room);
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
