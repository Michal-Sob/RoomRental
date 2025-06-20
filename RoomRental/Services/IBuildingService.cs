using RoomRental.Models;
using RoomRental.Models.Dtos;

namespace RoomRental.Services;

public interface IBuildingService
{
    Task<IEnumerable<BuildingListDto>> GetAllBuildingsAsync();
    Task<BuildingDto?> GetBuildingWithRoomsAsync(int id);
}