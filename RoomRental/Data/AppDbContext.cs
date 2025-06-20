using Microsoft.EntityFrameworkCore;
using RoomRental.Models;

namespace RoomRental.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Building> Buildings { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<AdditionalService> AdditionalServices { get; set; }
    public DbSet<Catering> Caterings { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>()
            .HasMany(r => r.Equipment)
            .WithMany(e => e.Rooms)
            .UsingEntity<Dictionary<string, object>>(
                "RoomEquipment",
                j => j.HasOne<Equipment>().WithMany().HasForeignKey("EquipmentId"),
                j => j.HasOne<Room>().WithMany().HasForeignKey("RoomId"));

        modelBuilder.Entity<Catering>()
            .HasMany(c => c.MenuItems)
            .WithMany(m => m.Caterings)
            .UsingEntity<Dictionary<string, object>>(
                "CateringMenuItem",
                j => j.HasOne<MenuItem>().WithMany().HasForeignKey("MenuItemId"),
                j => j.HasOne<Catering>().WithMany().HasForeignKey("CateringId"));

        modelBuilder.Entity<AdditionalService>()
            .HasDiscriminator<string>("ServiceType")
            .HasValue<Catering>("Catering");

        modelBuilder.Entity<Equipment>()
            .HasIndex(e => e.SerialNumber)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Department>()
            .HasIndex(d => d.Code)
            .IsUnique();

        modelBuilder.Entity<Document>()
            .HasIndex(d => d.DocumentNumber)
            .IsUnique();

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Room)
            .WithMany(room => room.Reservations)
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Department>()
            .Property(d => d.Budget)
            .HasPrecision(18, 2);

        modelBuilder.Entity<AdditionalService>()
            .Property(s => s.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<MenuItem>()
            .Property(m => m.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Document>()
            .Property(d => d.Amount)
            .HasPrecision(18, 2);

        SeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasData(
            new Department
            {
                Id = 1, Name = "Information Technology", Code = "IT", Manager = "John Smith", Budget = 50000m,
                CreatedAt = DateTime.UtcNow
            },
            new Department
            {
                Id = 2, Name = "Human Resources", Code = "HR", Manager = "Anna Johnson", Budget = 30000m,
                CreatedAt = DateTime.UtcNow
            },
            new Department
            {
                Id = 3, Name = "Sales", Code = "SALES", Manager = "Peter Williams", Budget = 40000m,
                CreatedAt = DateTime.UtcNow
            }
        );
        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1, FirstName = "Admin", LastName = "System", Email = "admin@company.com", Phone = "123456789",
                Role = UserRole.Administrator, DepartmentId = 1, CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = 2, FirstName = "Marcus", LastName = "Brown", Email = "m.brown@company.com",
                Phone = "987654321", Role = UserRole.RegularUser, DepartmentId = 2, CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = 3, FirstName = "Catherine", LastName = "Davis", Email = "c.davis@company.com",
                Phone = "555666777", Role = UserRole.Moderator, DepartmentId = 3, CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Building>().HasData(
            new Building
            {
                Id = 1, Name = "Building A", Address = "123 Main St, New York", NumberOfFloors = 5,
                Status = BuildingStatus.Open, OpeningTime = new TimeSpan(7, 0, 0),
                ClosingTime = new TimeSpan(22, 0, 0), CreatedAt = DateTime.UtcNow
            },
            new Building
            {
                Id = 2, Name = "Building B", Address = "456 Business Ave, Chicago", NumberOfFloors = 3,
                Status = BuildingStatus.Open, OpeningTime = new TimeSpan(8, 0, 0),
                ClosingTime = new TimeSpan(20, 0, 0), CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Room>().HasData(
            new Room
            {
                Id = 1, Name = "Conference Room 101", Capacity = 20, Floor = 1, Type = RoomType.Regular,
                Status = RoomStatus.Available, BuildingId = 1, CreatedAt = DateTime.UtcNow
            },
            new Room
            {
                Id = 2, Name = "VIP Conference Suite", Capacity = 50, Floor = 2, Type = RoomType.VIP,
                Status = RoomStatus.Available, BuildingId = 1, CreatedAt = DateTime.UtcNow
            },
            new Room
            {
                Id = 3, Name = "Auditorium", Capacity = 100, Floor = 1, Type = RoomType.Lecture,
                Status = RoomStatus.Available, BuildingId = 2, CreatedAt = DateTime.UtcNow
            },
            new Room
            {
                Id = 4, Name = "Meeting Room 102", Capacity = 15, Floor = 1, Type = RoomType.Regular,
                Status = RoomStatus.Available, BuildingId = 1, CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Equipment>().HasData(
            new Equipment
            {
                Id = 1, Name = "HD Projector", SerialNumber = "PRJ001", Manufacturer = "Epson",
                PurchaseDate = DateTime.UtcNow.AddYears(-2), Status = EquipmentStatus.Working,
                CreatedAt = DateTime.UtcNow
            },
            new Equipment
            {
                Id = 2, Name = "4K Projector", SerialNumber = "PRJ002", Manufacturer = "BenQ",
                PurchaseDate = DateTime.UtcNow.AddYears(-1), Status = EquipmentStatus.Working,
                CreatedAt = DateTime.UtcNow
            },
            new Equipment
            {
                Id = 3, Name = "Air Conditioning", SerialNumber = "AC001", Manufacturer = "Daikin",
                PurchaseDate = DateTime.UtcNow.AddYears(-3), Status = EquipmentStatus.Working,
                CreatedAt = DateTime.UtcNow
            },
            new Equipment
            {
                Id = 4, Name = "Sound System", SerialNumber = "SND001", Manufacturer = "Bose",
                PurchaseDate = DateTime.UtcNow.AddMonths(-6), Status = EquipmentStatus.Working,
                CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem
            {
                Id = 1,
                Name = "Mixed Sandwiches",
                Description = "Variety of fresh sandwiches",
                Price = 25.00m,
                Category = MenuCategory.MainCourse,
                CreatedAt = DateTime.UtcNow,
                Allergens = "gluten, eggs, milk"
            },
            new MenuItem
            {
                Id = 2,
                Name = "Caesar Salad",
                Description = "Fresh salad with chicken",
                Price = 18.00m,
                Category = MenuCategory.Appetizer,
                CreatedAt = DateTime.UtcNow,
                Allergens = "eggs, milk, fish"
            },
            new MenuItem
            {
                Id = 3,
                Name = "Coffee",
                Description = "Freshly brewed coffee",
                Price = 8.00m,
                Category = MenuCategory.Beverage,
                CreatedAt = DateTime.UtcNow,
                Allergens = ""
            },
            new MenuItem
            {
                Id = 4,
                Name = "Chocolate Cake",
                Description = "Homemade chocolate cake",
                Price = 12.00m,
                Category = MenuCategory.Dessert,
                CreatedAt = DateTime.UtcNow,
                Allergens = "gluten, eggs, milk, soy"
            }
        );
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((BaseEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                ((BaseEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}