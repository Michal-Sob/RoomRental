namespace RoomRental.Models.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int Floor { get; set; }
    public int Type { get; set; }
    public int Status { get; set; }
    public int BuildingId { get; set; }
    public string? BuildingName { get; set; }
}

public class RoomDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int Floor { get; set; }
    public int Type { get; set; }
    public int Status { get; set; }
    public int BuildingId { get; set; }
    public BuildingListDto? Building { get; set; }
    public List<EquipmentDto>? Equipment { get; set; }
}