using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomRental.Models;

public class Room : BaseEntity
{
    protected Room() {}
    
    public Room(Building building)
    {
        Building = building ?? throw new ArgumentNullException(nameof(building));
    }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Range(1, 1000)]
    public int Capacity { get; set; }

    [Range(0, 50)]
    public int Floor { get; set; }

    public RoomType Type { get; set; }
    public RoomStatus Status { get; set; }

    public int BuildingId { get; set; }

    [ForeignKey("BuildingId")]
    public virtual Building Building { get; set; }
    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public bool IsAvailable(DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        if (Status != RoomStatus.Available) return false;

        return !Reservations.Any(r => r.Date.Date == date.Date &&
                                      r.Status != ReservationStatus.Cancelled &&
                                      ((r.StartTime <= startTime && r.EndTime > startTime) ||
                                       (r.StartTime < endTime && r.EndTime >= endTime) ||
                                       (r.StartTime >= startTime && r.EndTime <= endTime)));
    }

    public decimal CalculateCost(TimeSpan duration)
    {
        decimal hourlyRate = Type switch
        {
            RoomType.Regular => 50m,
            RoomType.Lecture => 75m,
            RoomType.VIP => 150m,
            _ => 50m
        };

        return hourlyRate * (decimal)duration.TotalHours;
    }
}