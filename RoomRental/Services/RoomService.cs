using Microsoft.EntityFrameworkCore;
using RoomRental.Data;
using RoomRental.Models;
using RoomRental.Models.Dtos;

namespace RoomRental.Services;

public class RoomService : IRoomService
{
    private readonly AppDbContext _context;

    public RoomService(AppDbContext context)
    {
        _context = context;
    }

public async Task<IEnumerable<RoomDto>> GetRoomsByBuildingAsync(int buildingId)
    {
        return await _context.Rooms
            .Where(r => r.BuildingId == buildingId && r.Status == RoomStatus.Available)
            .Select(r => new RoomDto
            {
                Id = r.Id,
                Name = r.Name,
                Capacity = r.Capacity,
                Floor = r.Floor,
                Type = (int)r.Type,
                Status = (int)r.Status,
                BuildingId = r.BuildingId,
                BuildingName = r.Building.Name
            })
            .ToListAsync();
    }

    public async Task<RoomDetailsDto?> GetRoomAsync(int roomId)
    {
        return await _context.Rooms
            .Where(r => r.Id == roomId)
            .Select(r => new RoomDetailsDto
            {
                Id = r.Id,
                Name = r.Name,
                Capacity = r.Capacity,
                Floor = r.Floor,
                Type = (int)r.Type,
                Status = (int)r.Status,
                BuildingId = r.BuildingId,
                Building = new BuildingListDto
                {
                    Id = r.Building.Id,
                    Name = r.Building.Name,
                    Address = r.Building.Address,
                    NumberOfFloors = r.Building.NumberOfFloors,
                    Status = (int)r.Building.Status,
                    OpeningTime = r.Building.OpeningTime.ToString(@"hh\:mm"),
                    ClosingTime = r.Building.ClosingTime.ToString(@"hh\:mm")
                },
                Equipment = r.Equipment.Select(e => new EquipmentDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    SerialNumber = e.SerialNumber,
                    Manufacturer = e.Manufacturer,
                    PurchaseDate = e.PurchaseDate,
                    Status = (int)e.Status
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }
    
    /// <summary>
    /// Retrieves a list of available rooms for a given date and time range.
    /// </summary>
    /// <param name="date"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        var availableRoomIds = await _context.Rooms
            .Where(r => r.Status == RoomStatus.Available)
            .Where(r => !r.Reservations.Any(res => 
                res.Date.Date == date.Date &&
                res.Status != ReservationStatus.Cancelled &&
                ((res.StartTime <= startTime && res.EndTime > startTime) ||
                 (res.StartTime < endTime && res.EndTime >= endTime) ||
                 (res.StartTime >= startTime && res.EndTime <= endTime))))
            .Select(r => r.Id)
            .ToListAsync();

        return await _context.Rooms
            .Where(r => availableRoomIds.Contains(r.Id))
            .Select(r => new RoomDto
            {
                Id = r.Id,
                Name = r.Name,
                Capacity = r.Capacity,
                Floor = r.Floor,
                Type = (int)r.Type,
                Status = (int)r.Status,
                BuildingId = r.BuildingId,
                BuildingName = r.Building.Name
            })
            .ToListAsync();
    }
    
        public async Task<RoomDto> CreateRoomAsync(RoomDto roomDto)
    {
            // Validation
            if (string.IsNullOrWhiteSpace(roomDto.Name))
                throw new ArgumentException("Room name is required");

            if (roomDto.Capacity <= 0)
                throw new ArgumentException("Capacity must be greater than 0");

            if (roomDto.Floor < 0)
                throw new ArgumentException("Floor must be 0 or greater");

            
            var building = await _context.Buildings.FindAsync(roomDto.BuildingId);
            if (building == null)
                throw new ArgumentException("Building not found");

            // is room name unique in the building
            var existingRoom = await _context.Rooms
                .AnyAsync(r => r.BuildingId == roomDto.BuildingId && 
                              r.Name.ToLower() == roomDto.Name.ToLower());

            if (existingRoom)
                throw new InvalidOperationException("A room with this name already exists in this building");

            if (roomDto.Floor > building.NumberOfFloors)
                throw new ArgumentException($"Floor cannot exceed building's number of floors ({building.NumberOfFloors})");

            var room = new Room(building)
            {
                Name = roomDto.Name.Trim(),
                Capacity = roomDto.Capacity,
                Floor = roomDto.Floor,
                Type = (RoomType)roomDto.Type,
                Status = RoomStatus.Available,
                BuildingId = building.Id
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity,
                Floor = room.Floor,
                Type = (int)room.Type,
                Status = (int)room.Status,
                BuildingId = room.BuildingId,
                BuildingName = building.Name,
            };
    }
}
    