using Microsoft.EntityFrameworkCore;
using RoomRental.Models;

namespace RoomRental.Data;

public static class DatabaseSeeder
{
    public static async Task SeedRoomsAsync(AppDbContext context)
    {
        if (await context.Rooms.AnyAsync()) 
            return;

        var building1 = await context.Buildings.FirstOrDefaultAsync(b => b.Id == 1);
        var building2 = await context.Buildings.FirstOrDefaultAsync(b => b.Id == 2);

        if (building1 == null || building2 == null)
        {
            throw new InvalidOperationException("Buildings must be seeded before rooms");
        }

        var rooms = new List<Room>
        {
            new Room(building1) 
            { 
                Name = "Conference Room 101", 
                Capacity = 20, 
                Floor = 1, 
                Type = RoomType.Regular,
                Status = RoomStatus.Available
            },
            new Room(building1) 
            { 
                Name = "VIP Conference Suite", 
                Capacity = 50, 
                Floor = 2, 
                Type = RoomType.VIP,
                Status = RoomStatus.Available
            },
            new Room(building2) 
            { 
                Name = "Auditorium", 
                Capacity = 100, 
                Floor = 1, 
                Type = RoomType.Lecture,
                Status = RoomStatus.Available
            },
            new Room(building1) 
            { 
                Name = "Meeting Room 102", 
                Capacity = 15, 
                Floor = 1, 
                Type = RoomType.Regular,
                Status = RoomStatus.Available
            }
        };

        context.Rooms.AddRange(rooms);
        await context.SaveChangesAsync();
    }
}