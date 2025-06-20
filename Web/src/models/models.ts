export enum RoomStatus {
  Available = 0,
  UnderMaintenance = 1,
  Reserved = 2
}

export enum RoomType {
  Regular = 0,
  Lecture = 1,
  VIP = 2
}

export enum BuildingStatus {
  Open = 0,
  Closed = 1,
  UnderConstruction = 2
}

export enum ReservationStatus {
  Pending = 0,
  Confirmed = 1,
  Cancelled = 2
}

export interface Building {
  id: number;
  name: string;
  address: string;
  numberOfFloors: number;
  status: BuildingStatus;
  openingTime: string;
  closingTime: string;
  rooms?: Room[];
}

export interface Room {
  id: number;
  name: string;
  capacity: number;
  floor: number;
  type: RoomType;
  status: RoomStatus;
  buildingId: number;
  building?: Building;
}

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  departmentId: number;
}

export interface Reservation {
  id: number;
  date: string;
  startTime: string;
  endTime: string;
  status: ReservationStatus;
  userId: number;
  user?: User;
  roomId: number;
  room?: Room;
}

export interface CreateReservationDto {
  date: string;
  startTime: string;
  endTime: string;
  userId: number;
  roomId: number;
}
