using System.ComponentModel.DataAnnotations;

namespace RoomRental.Models;

public class Meeting : BaseEntity
{
    [Required]
    [StringLength(200)]
    public string Topic { get; set; }

    [Range(1, 1000)]
    public int NumberOfParticipants { get; set; }

    [StringLength(1000)]
    public string ParticipantsList { get; set; }

    // Navigation properties
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public bool ValidateCapacity(Room room)
    {
        return room.Capacity >= NumberOfParticipants;
    }
}