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

  getRoomsByBuilding(buildingId: number): Observable<RoomDto[]> {
    return this.http.get<RoomDto[]>(`${this.baseUrl}/rooms/building/${buildingId}`);
  }

  getReservations(): Observable<ReservationDto[]> {
    return this.http.get<ReservationDto[]>(`${this.baseUrl}/reservations`);
  }

  createReservation(reservation: CreateReservationDto): Observable<ReservationDto> {
    return this.http.post<ReservationDto>(`${this.baseUrl}/reservations`, reservation);
  }

  cancelReservation(reservationId: number): Observable<{message: string}> {
    return this.http.put<{message: string}>(`${this.baseUrl}/reservations/${reservationId}/cancel`, {});
  }

  deleteReservation(reservationId: number): Observable<{message: string}> {
    return this.http.delete<{message: string}>(`${this.baseUrl}/reservations/${reservationId}`);
  }

  createRoom(room: RoomDto): Observable<RoomDto> {
    return this.http.post<RoomDto>(`${this.baseUrl}/rooms`, room);
  }
}
