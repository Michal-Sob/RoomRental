import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Building, Room, Reservation, CreateReservationDto } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'https://localhost:7206/api';

  constructor(private http: HttpClient) { }

  getBuildings(): Observable<Building[]> {
    return this.http.get<Building[]>(`${this.baseUrl}/buildings`);
  }


  getBuildingWithRooms(buildingId: number): Observable<Building> {
    return this.http.get<Building>(`${this.baseUrl}/buildings/${buildingId}/rooms`);
  }

  getRoomsByBuilding(buildingId: number): Observable<Room[]> {
    return this.http.get<Room[]>(`${this.baseUrl}/rooms/building/${buildingId}`);
  }


  getReservations(): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(`${this.baseUrl}/reservations`);
  }

  createReservation(reservation: CreateReservationDto): Observable<Reservation> {
    return this.http.post<Reservation>(`${this.baseUrl}/reservations`, reservation);
  }
}
