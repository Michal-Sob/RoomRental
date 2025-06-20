namespace RoomRental.Models.Dtos;

public class BuildingDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int NumberOfFloors { get; set; }
    public int Status { get; set; }
    public string OpeningTime { get; set; }
    public string ClosingTime { get; set; }
    public List<RoomDto>? Rooms { get; set; }
}

public class BuildingListDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int NumberOfFloors { get; set; }
    public int Status { get; set; }
    public string OpeningTime { get; set; }
    public string ClosingTime { get; set; }
}