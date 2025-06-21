using Microsoft.AspNetCore.Mvc;
using RoomRental.Models.Dtos;
using RoomRental.Services;

namespace RoomRental.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetReservations()
    {
        try
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving reservations", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservation(int id)
    {
        try
        {
            var reservation = await _reservationService.GetReservationAsync(id);
            if (reservation == null)
                return NotFound(new { message = "Reservation not found" });

            return Ok(reservation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving reservation", error = ex.Message });
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetReservationsByUser(int userId)
    {
        try
        {
            var reservations = await _reservationService.GetReservationsByUserAsync(userId);
            return Ok(reservations);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving user reservations", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
    {
        try
        {
            var reservation = await _reservationService.CreateReservationAsync(dto);
            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating reservation", error = ex.Message });
        }
    }
    
    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelReservation(int id)
    {
        try
        {
            var canCancel = await _reservationService.CanCancelReservationAsync(id);
            if (!canCancel)
            {
                return BadRequest(new { message = "This reservation cannot be cancelled. Reservations can only be cancelled if they are confirmed and at least 2 hours before the start time." });
            }

            var success = await _reservationService.CancelReservationAsync(id);
            if (!success)
            {
                return NotFound(new { message = "Reservation not found" });
            }

            return Ok(new { message = "Reservation cancelled successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error cancelling reservation", error = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        try
        {
            var success = await _reservationService.DeleteReservationAsync(id);
            
            if (!success)
            {
                return BadRequest(new { 
                    message = "This reservation cannot be deleted. Only cancelled reservations can be deleted." 
                });
            }

            return Ok(new { message = "Reservation deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting reservation", error = ex.Message });
        }
    }
}