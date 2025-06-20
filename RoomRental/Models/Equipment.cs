using System.ComponentModel.DataAnnotations;

namespace RoomRental.Models;

public class Equipment : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(50)]
    public string SerialNumber { get; set; }

    [StringLength(100)]
    public string Manufacturer { get; set; }

    public DateTime PurchaseDate { get; set; }
    public EquipmentStatus Status { get; set; }

    // Navigation properties
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}