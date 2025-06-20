namespace RoomRental.Models.Dtos;

public class ReservationDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public int Status { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public UserDto? User { get; set; }
    public RoomDto? Room { get; set; }
}

public class CreateReservationDto
{
    public DateTime Date { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
}

public class ReservationListDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public int Status { get; set; }
    public string UserName { get; set; }
    public string RoomName { get; set; }
    public string BuildingName { get; set; }
}

public class UpdateReservationDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public ReservationStatus Status { get; set; }
    public int? MeetingId { get; set; }
}