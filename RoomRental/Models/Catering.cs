using System.ComponentModel.DataAnnotations;

namespace RoomRental.Models;

public class Catering : AdditionalService
{
    [StringLength(100)]
    public string MealType { get; set; }

    [Range(1, 1000)]
    public int NumberOfPeople { get; set; }

    [StringLength(500)]
    public string DietaryRequirements { get; set; }

    public TimeSpan DeliveryTime { get; set; }

    // Navigation properties
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    public override bool Validate()
    {
        return NumberOfPeople > 0 && !string.IsNullOrEmpty(MealType);
    }
}