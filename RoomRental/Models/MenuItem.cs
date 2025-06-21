using System.ComponentModel.DataAnnotations;

namespace RoomRental.Models;

public class MenuItem : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    public MenuCategory Category { get; set; }

    [StringLength(200)]
    public string Allergens { get; set; }

    public virtual ICollection<Catering> Caterings { get; set; } = new List<Catering>();
}