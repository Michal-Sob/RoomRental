using Microsoft.AspNetCore.Mvc;
using RoomRental.Models.Dtos;
using RoomRental.Services;

namespace RoomRental.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet("building/{buildingId}")]
    public async Task<IActionResult> GetRoomsByBuilding(int buildingId)
    {
        try
        {
            var rooms = await _roomService.GetRoomsByBuildingAsync(buildingId);
            return Ok(rooms);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving rooms", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoom(int id)
    {
        try
        {
            var room = await _roomService.GetRoomAsync(id);
            if (room == null)
                return NotFound(new { message = "Room not found" });

            return Ok(room);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving room", error = ex.Message });
        }
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableRooms(
        [FromQuery] DateTime date,
        [FromQuery] string startTime,
        [FromQuery] string endTime)
    {
        try
        {
            if (!TimeSpan.TryParse(startTime, out var start) || !TimeSpan.TryParse(endTime, out var end))
            {
                return BadRequest(new { message = "Invalid time format" });
            }

            var rooms = await _roomService.GetAvailableRoomsAsync(date, start, end);
            return Ok(rooms);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving available rooms", error = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] RoomDto dto)
    {
        try
        {
            var room = await _roomService.CreateRoomAsync(dto);
            
            return StatusCode(201, room);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating room", error = ex.Message });
        }
    }
}