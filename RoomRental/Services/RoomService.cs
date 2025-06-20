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
}
    