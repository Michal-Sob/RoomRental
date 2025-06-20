import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateReservationDto, ReservationDto, BuildingDto, RoomDto, BuildingListDto } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'https://localhost:7206/api';

  constructor(private http: HttpClient) { }

  getBuildings(): Observable<BuildingListDto[]> {
    return this.http.get<BuildingListDto[]>(`${this.baseUrl}/buildings`);
  }


  getBuildingWithRooms(buildingId: number): Observable<BuildingDto> {
    return this.http.get<BuildingDto>(`${this.baseUrl}/buildings/${buildingId}/rooms`);
  }

  getRoomsByBuilding(buildingId: number): Observable<RoomDto[]> {
    return this.http.get<RoomDto[]>(`${this.baseUrl}/rooms/building/${buildingId}`);
  }


  getReservations(): Observable<ReservationDto[]> {
    return this.http.get<ReservationDto[]>(`${this.baseUrl}/reservations`);
  }

  createReservation(reservation: CreateReservationDto): Observable<ReservationDto> {
    return this.http.post<ReservationDto>(`${this.baseUrl}/reservations`, reservation);
  }
}
