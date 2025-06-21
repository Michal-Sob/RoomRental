using RoomRental.Models;
using RoomRental.Models.Dtos;

namespace RoomRental.Services;

    public interface IReservationService
    {
        
        Task<ReservationDto> CreateReservationAsync(CreateReservationDto dto);
        
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task<IEnumerable<ReservationListDto>> GetReservationsByUserAsync(int userId);
        Task<ReservationDto?> GetReservationAsync(int id);
        
        Task<bool> CancelReservationAsync(int reservationId);
        Task<bool> CanCancelReservationAsync(int reservationId);
        Task<bool> DeleteReservationAsync(int reservationId);
    }