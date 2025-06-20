using RoomRental.Models;
using RoomRental.Models.Dtos;

namespace RoomRental.Services;

    public interface IReservationService
    {
        
        Task<ReservationDto> CreateReservationAsync(CreateReservationDto dto);
        Task<IEnumerable<ReservationListDto>> GetAllReservationsAsync();
        Task<IEnumerable<ReservationListDto>> GetReservationsByUserAsync(int userId);
        Task<ReservationDto?> GetReservationAsync(int id);

    }