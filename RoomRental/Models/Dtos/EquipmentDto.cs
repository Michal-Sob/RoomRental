namespace RoomRental.Models.Dtos;

public class EquipmentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string Manufacturer { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int Status { get; set; }
}