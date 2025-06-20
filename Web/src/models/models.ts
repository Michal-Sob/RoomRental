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

export interface BuildingListDto {
  id: number;
  name: string;
  address: string;
  numberOfFloors: number;
  status: BuildingStatus;
  openingTime: string;
  closingTime: string;
}

export interface BuildingDto {
  id: number;
  name: string;
  address: string;
  numberOfFloors: number;
  status: BuildingStatus;
  openingTime: string;
  closingTime: string;
  rooms?: RoomDto[];
}

export interface RoomDto {
  id: number;
  name: string;
  capacity: number;
  floor: number;
  type: RoomType;
  status: RoomStatus;
  buildingId: number;
  buildingName?: string;
}

export interface RoomDetailsDto {
  id: number;
  name: string;
  capacity: number;
  floor: number;
  type: RoomType;
  status: RoomStatus;
  buildingId: number;
  building?: BuildingListDto;
}

export interface ReservationDto {
  id: number;
  date: string;
  startTime: string;
  endTime: string;
  status: ReservationStatus;
  userId: number;
  roomId: number;
  room?: RoomDto;
}

export interface ReservationListDto {
  id: number;
  date: string;
  startTime: string;
  endTime: string;
  status: ReservationStatus;
  userName: string;
  roomName: string;
  buildingName: string;
}

export interface CreateReservationDto {
  date: string;
  startTime: string;
  endTime: string;
  userId: number;
  roomId: number;
}



