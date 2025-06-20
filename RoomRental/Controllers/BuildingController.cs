using Microsoft.AspNetCore.Mvc;
using RoomRental.Services;

namespace RoomRental.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildingsController : ControllerBase
{
    private readonly IBuildingService _buildingService;

    public BuildingsController(IBuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBuildings()
    {
        try
        {
            var buildings = await _buildingService.GetAllBuildingsAsync();
            return Ok(buildings);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving buildings", error = ex.Message });
        }
    }

    [HttpGet("{id}/rooms")]
    public async Task<IActionResult> GetBuildingWithRooms(int id)
    {
        try
        {
            var building = await _buildingService.GetBuildingWithRoomsAsync(id);
            if (building == null)
                return NotFound(new { message = "Building not found" });

            return Ok(building);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving building", error = ex.Message });
        }
    }
}