using System.ComponentModel.DataAnnotations;

namespace RoomRental.Models;

public class Department : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(10)]
    public string Code { get; set; }

    [StringLength(100)]
    public string Manager { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Budget { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}