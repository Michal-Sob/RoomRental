using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomRental.Models;

public class User : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Phone]
    [StringLength(20)]
    public string Phone { get; set; }

    public UserRole Role { get; set; }

    // Foreign Keys
    public int DepartmentId { get; set; }

    // Navigation properties
    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; }
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public bool ValidateMeetingData(DateTime date, TimeSpan duration, int numberOfPeople)
    {
        return date > DateTime.Now && duration > TimeSpan.Zero && numberOfPeople > 0;
    }

    public bool SelectEquipment(List<Equipment> equipmentList)
    {
        return equipmentList != null && equipmentList.All(e => e.Status == EquipmentStatus.Working);
    }

    public string FullName => $"{FirstName} {LastName}";
}