namespace RoomRental.Models.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int Role { get; set; }
    public int DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
}