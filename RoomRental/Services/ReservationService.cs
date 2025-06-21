using Microsoft.EntityFrameworkCore;
using RoomRental.Data;
using RoomRental.Models;
using RoomRental.Models.Dtos;

namespace RoomRental.Services;

public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;

        public ReservationService(AppDbContext context)
        {
            _context = context;
        }

 public async Task<ReservationDto> CreateReservationAsync(CreateReservationDto dto)
    {
        if (!TimeSpan.TryParse(dto.StartTime, out var startTime))
            throw new ArgumentException("Invalid start time format");

        if (!TimeSpan.TryParse(dto.EndTime, out var endTime))
            throw new ArgumentException("Invalid end time format");

        if (dto.Date < DateTime.Today)
            throw new ArgumentException("Cannot create reservation for past dates");

        if (startTime >= endTime)
            throw new ArgumentException("Start time must be before end time");

        var isAvailable = !await _context.Reservations
            .AnyAsync(r => r.RoomId == dto.RoomId && 
                          r.Date.Date == dto.Date.Date &&
                          r.Status != ReservationStatus.Cancelled &&
                          ((r.StartTime <= startTime && r.EndTime > startTime) ||
                           (r.StartTime < endTime && r.EndTime >= endTime) ||
                           (r.StartTime >= startTime && r.EndTime <= endTime)));

        if (!isAvailable)
            throw new InvalidOperationException("Room is not available at the selected time");

        var reservation = new Reservation
        {
            Date = dto.Date,
            StartTime = startTime,
            EndTime = endTime,
            Status = ReservationStatus.Confirmed,
            UserId = dto.UserId,
            RoomId = dto.RoomId
        };

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        return await GetReservationAsync(reservation.Id) ?? 
            throw new InvalidOperationException("Failed to retrieve created reservation");
    }

    public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
    {
        return await _context.Reservations
            .Include(r => r.User)
            .ThenInclude(u => u.Department)
            .Include(r => r.Room)
            .ThenInclude(room => room.Building)
            .OrderByDescending(r => r.Date)
            .ThenBy(r => r.StartTime)
            .Select(r => new ReservationDto
            {
                Id = r.Id,
                Date = r.Date,
                StartTime = r.StartTime.ToString(@"hh\:mm"),
                EndTime = r.EndTime.ToString(@"hh\:mm"),
                Status = (int)r.Status,
                UserId = r.UserId,
                RoomId = r.RoomId,
                Room = new RoomDto
                {
                    Id = r.Room.Id,
                    Name = r.Room.Name,
                    Capacity = r.Room.Capacity,
                    Floor = r.Room.Floor,
                    Type = (int)r.Room.Type,
                    Status = (int)r.Room.Status,
                    BuildingId = r.Room.BuildingId,
                    BuildingName = r.Room.Building.Name
                }
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<ReservationListDto>> GetReservationsByUserAsync(int userId)
    {
        return await _context.Reservations
            .Where(r => r.UserId == userId)
            .Select(r => new ReservationListDto
            {
                Id = r.Id,
                Date = r.Date,
                StartTime = r.StartTime.ToString(@"hh\:mm"),
                EndTime = r.EndTime.ToString(@"hh\:mm"),
                Status = (int)r.Status,
                UserName = $"{r.User.FirstName} {r.User.LastName}",
                RoomName = r.Room.Name,
                BuildingName = r.Room.Building.Name
            })
            .OrderByDescending(r => r.Date)
            .ThenBy(r => r.StartTime)
            .ToListAsync();
    }

    public async Task<ReservationDto?> GetReservationAsync(int id)
    {
        return await _context.Reservations
            .Where(r => r.Id == id)
            .Select(r => new ReservationDto
            {
                Id = r.Id,
                Date = r.Date,
                StartTime = r.StartTime.ToString(@"hh\:mm"),
                EndTime = r.EndTime.ToString(@"hh\:mm"),
                Status = (int)r.Status,
                UserId = r.UserId,
                RoomId = r.RoomId,
                User = new UserDto
                {
                    Id = r.User.Id,
                    FirstName = r.User.FirstName,
                    LastName = r.User.LastName,
                    Email = r.User.Email,
                    Phone = r.User.Phone,
                    Role = (int)r.User.Role,
                    DepartmentId = r.User.DepartmentId,
                    DepartmentName = r.User.Department.Name
                },
                Room = new RoomDto
                {
                    Id = r.Room.Id,
                    Name = r.Room.Name,
                    Capacity = r.Room.Capacity,
                    Floor = r.Room.Floor,
                    Type = (int)r.Room.Type,
                    Status = (int)r.Room.Status,
                    BuildingId = r.Room.BuildingId,
                    BuildingName = r.Room.Building.Name
                }
            })
            .FirstOrDefaultAsync();
    }
    
    public async Task<bool> CancelReservationAsync(int reservationId)
    {
        var reservation = await _context.Reservations
            .Include(r => r.Room)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == reservationId);

        if (reservation == null)
            return false;

        if (!await CanCancelReservationAsync(reservationId))
            return false;

        reservation.Status = ReservationStatus.Cancelled;
        reservation.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CanCancelReservationAsync(int reservationId)
    {
        var reservation = await _context.Reservations
            .FirstOrDefaultAsync(r => r.Id == reservationId);

        if (reservation == null)
            return false;
        if (reservation.Status != ReservationStatus.Confirmed)
            return false;

        var reservationDateTime = reservation.Date.Add(reservation.StartTime);
        var hoursUntilReservation = (reservationDateTime - DateTime.Now).TotalHours;

        return hoursUntilReservation > 2;
    }
    
    public async Task<bool> DeleteReservationAsync(int reservationId)
    {
        var reservation = await _context.Reservations
            .FirstOrDefaultAsync(r => r.Id == reservationId);

        if (reservation == null)
            return false;

        // Only cancelled reservations
        if (reservation.Status != ReservationStatus.Cancelled)
            return false;

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
        return true;
    }
}