using Microsoft.EntityFrameworkCore;
using RoomRental.Data;
using RoomRental.Models;
using RoomRental.Models.Dtos;

namespace RoomRental.Services;

public class BuildingService : IBuildingService
{
    private readonly AppDbContext _context;

    public BuildingService(AppDbContext context)
    {
        _context = context;
    }

    
    /// <summary>
    /// Retrieves a list of all buildings that are currently open.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<BuildingListDto>> GetAllBuildingsAsync()
    {
        return await _context.Buildings
            .Where(b => b.Status == BuildingStatus.Open)
            .Select(b => new BuildingListDto
            {
                Id = b.Id,
                Name = b.Name,
                Address = b.Address,
                NumberOfFloors = b.NumberOfFloors,
                Status = (int)b.Status,
                OpeningTime = b.OpeningTime.ToString(@"hh\:mm"),
                ClosingTime = b.ClosingTime.ToString(@"hh\:mm")
            })
            .ToListAsync();
    }

    
    /// <summary>
    /// Retrieves a specific building by its ID, including its rooms.
    /// </summary>
    /// <param name="buildingId"></param>
    /// <returns></returns>
    public async Task<BuildingDto?> GetBuildingWithRoomsAsync(int buildingId)
    {
        var building = await _context.Buildings
            .Where(b => b.Id == buildingId)
            .Select(b => new BuildingDto
            {
                Id = b.Id,
                Name = b.Name,
                Address = b.Address,
                NumberOfFloors = b.NumberOfFloors,
                Status = (int)b.Status,
                OpeningTime = b.OpeningTime.ToString(@"hh\:mm"),
                ClosingTime = b.ClosingTime.ToString(@"hh\:mm"),
                Rooms = b.Rooms
                    .Where(r => r.Status == RoomStatus.Available)
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
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        return building;
    }
}