using RoomRental.Models;
using RoomRental.Models.Dtos;

namespace RoomRental.Services;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetRoomsByBuildingAsync(int buildingId);
    Task<RoomDetailsDto?> GetRoomAsync(int roomId);
    Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime);
}