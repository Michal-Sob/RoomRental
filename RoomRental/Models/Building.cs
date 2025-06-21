using System.ComponentModel.DataAnnotations;

namespace RoomRental.Models;

public class Building : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(200)]
    public string Address { get; set; }

    [Range(1, 50)]
    public int NumberOfFloors { get; set; }

    public BuildingStatus Status { get; set; }

    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public bool IsOpen(DateTime dateTime)
    {
        return Status == BuildingStatus.Open && 
               dateTime.TimeOfDay >= OpeningTime && 
               dateTime.TimeOfDay <= ClosingTime;
    }
}