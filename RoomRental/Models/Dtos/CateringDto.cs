namespace RoomRental.Models.Dtos;

public class CateringDto
{
    public string MealType { get; set; }
    public int NumberOfPeople { get; set; }
    public string? DietaryRequirements { get; set; }
    public TimeSpan DeliveryTime { get; set; }
    public List<int> MenuItemIds { get; set; } = new List<int>();
}