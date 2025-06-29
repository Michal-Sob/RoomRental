﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoomRental.Data;

#nullable disable

namespace RoomRental.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250619153408_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CateringMenuItem", b =>
                {
                    b.Property<int>("CateringId")
                        .HasColumnType("int");

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.HasKey("CateringId", "MenuItemId");

                    b.HasIndex("MenuItemId");

                    b.ToTable("CateringMenuItem");
                });

            modelBuilder.Entity("RoomEquipment", b =>
                {
                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("EquipmentId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomEquipment");
                });

            modelBuilder.Entity("RoomRental.Models.AdditionalService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<TimeSpan>("PreparationTime")
                        .HasColumnType("time");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.ToTable("AdditionalServices");

                    b.HasDiscriminator<string>("ServiceType").HasValue("AdditionalService");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("RoomRental.Models.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<TimeSpan>("ClosingTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("NumberOfFloors")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("OpeningTime")
                        .HasColumnType("time");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Buildings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "123 Main St, New York",
                            ClosingTime = new TimeSpan(0, 22, 0, 0, 0),
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(5494),
                            Name = "Building A",
                            NumberOfFloors = 5,
                            OpeningTime = new TimeSpan(0, 7, 0, 0, 0),
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            Address = "456 Business Ave, Chicago",
                            ClosingTime = new TimeSpan(0, 20, 0, 0, 0),
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(5513),
                            Name = "Building B",
                            NumberOfFloors = 3,
                            OpeningTime = new TimeSpan(0, 8, 0, 0, 0),
                            Status = 0
                        });
                });

            modelBuilder.Entity("RoomRental.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Budget")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Manager")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Budget = 50000m,
                            Code = "IT",
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(597),
                            Manager = "John Smith",
                            Name = "Information Technology"
                        },
                        new
                        {
                            Id = 2,
                            Budget = 30000m,
                            Code = "HR",
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(688),
                            Manager = "Anna Johnson",
                            Name = "Human Resources"
                        },
                        new
                        {
                            Id = 3,
                            Budget = 40000m,
                            Code = "SALES",
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(689),
                            Manager = "Peter Williams",
                            Name = "Sales"
                        });
                });

            modelBuilder.Entity("RoomRental.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DocumentNumber")
                        .IsUnique();

                    b.HasIndex("ReservationId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("RoomRental.Models.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SerialNumber")
                        .IsUnique();

                    b.ToTable("Equipment");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(7521),
                            Manufacturer = "Epson",
                            Name = "HD Projector",
                            PurchaseDate = new DateTime(2023, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(7292),
                            SerialNumber = "PRJ001",
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(7529),
                            Manufacturer = "BenQ",
                            Name = "4K Projector",
                            PurchaseDate = new DateTime(2024, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(7525),
                            SerialNumber = "PRJ002",
                            Status = 0
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(7531),
                            Manufacturer = "Daikin",
                            Name = "Air Conditioning",
                            PurchaseDate = new DateTime(2022, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(7531),
                            SerialNumber = "AC001",
                            Status = 0
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(7549),
                            Manufacturer = "Bose",
                            Name = "Sound System",
                            PurchaseDate = new DateTime(2024, 12, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(7532),
                            SerialNumber = "SND001",
                            Status = 0
                        });
                });

            modelBuilder.Entity("RoomRental.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfParticipants")
                        .HasColumnType("int");

                    b.Property<string>("ParticipantsList")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("RoomRental.Models.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Allergens")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MenuItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Allergens = "gluten, eggs, milk",
                            Category = 0,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(8415),
                            Description = "Variety of fresh sandwiches",
                            Name = "Mixed Sandwiches",
                            Price = 25.00m
                        },
                        new
                        {
                            Id = 2,
                            Allergens = "eggs, milk, fish",
                            Category = 1,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(8499),
                            Description = "Fresh salad with chicken",
                            Name = "Caesar Salad",
                            Price = 18.00m
                        },
                        new
                        {
                            Id = 3,
                            Allergens = "",
                            Category = 3,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(8500),
                            Description = "Freshly brewed coffee",
                            Name = "Coffee",
                            Price = 8.00m
                        },
                        new
                        {
                            Id = 4,
                            Allergens = "gluten, eggs, milk, soy",
                            Category = 2,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(8502),
                            Description = "Homemade chocolate cake",
                            Name = "Chocolate Cake",
                            Price = 12.00m
                        });
                });

            modelBuilder.Entity("RoomRental.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelivered")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("RoomRental.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Criteria")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("GeneratedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("RoomRental.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<int?>("MeetingId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeetingId");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("RoomRental.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BuildingId")
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BuildingId = 1,
                            Capacity = 20,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(6690),
                            Floor = 1,
                            Name = "Conference Room 101",
                            Status = 0,
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            BuildingId = 1,
                            Capacity = 50,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(6694),
                            Floor = 2,
                            Name = "VIP Conference Suite",
                            Status = 0,
                            Type = 2
                        },
                        new
                        {
                            Id = 3,
                            BuildingId = 2,
                            Capacity = 100,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(6696),
                            Floor = 1,
                            Name = "Auditorium",
                            Status = 0,
                            Type = 1
                        },
                        new
                        {
                            Id = 4,
                            BuildingId = 1,
                            Capacity = 15,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(6697),
                            Floor = 1,
                            Name = "Meeting Room 102",
                            Status = 0,
                            Type = 0
                        });
                });

            modelBuilder.Entity("RoomRental.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(4575),
                            DepartmentId = 1,
                            Email = "admin@company.com",
                            FirstName = "Admin",
                            LastName = "System",
                            Phone = "123456789",
                            Role = 2
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(4579),
                            DepartmentId = 2,
                            Email = "m.brown@company.com",
                            FirstName = "Marcus",
                            LastName = "Brown",
                            Phone = "987654321",
                            Role = 0
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2025, 6, 19, 15, 34, 7, 960, DateTimeKind.Utc).AddTicks(4580),
                            DepartmentId = 3,
                            Email = "c.davis@company.com",
                            FirstName = "Catherine",
                            LastName = "Davis",
                            Phone = "555666777",
                            Role = 1
                        });
                });

            modelBuilder.Entity("RoomRental.Models.Catering", b =>
                {
                    b.HasBaseType("RoomRental.Models.AdditionalService");

                    b.Property<TimeSpan>("DeliveryTime")
                        .HasColumnType("time");

                    b.Property<string>("DietaryRequirements")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("MealType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("NumberOfPeople")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Catering");
                });

            modelBuilder.Entity("CateringMenuItem", b =>
                {
                    b.HasOne("RoomRental.Models.Catering", null)
                        .WithMany()
                        .HasForeignKey("CateringId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoomRental.Models.MenuItem", null)
                        .WithMany()
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoomEquipment", b =>
                {
                    b.HasOne("RoomRental.Models.Equipment", null)
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoomRental.Models.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoomRental.Models.AdditionalService", b =>
                {
                    b.HasOne("RoomRental.Models.Reservation", "Reservation")
                        .WithMany("AdditionalServices")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("RoomRental.Models.Document", b =>
                {
                    b.HasOne("RoomRental.Models.Reservation", "Reservation")
                        .WithMany("Documents")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("RoomRental.Models.Notification", b =>
                {
                    b.HasOne("RoomRental.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RoomRental.Models.Reservation", b =>
                {
                    b.HasOne("RoomRental.Models.Meeting", "Meeting")
                        .WithMany("Reservations")
                        .HasForeignKey("MeetingId");

                    b.HasOne("RoomRental.Models.Room", "Room")
                        .WithMany("Reservations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RoomRental.Models.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Meeting");

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RoomRental.Models.Room", b =>
                {
                    b.HasOne("RoomRental.Models.Building", "Building")
                        .WithMany("Rooms")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("RoomRental.Models.User", b =>
                {
                    b.HasOne("RoomRental.Models.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("RoomRental.Models.Building", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("RoomRental.Models.Department", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("RoomRental.Models.Meeting", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("RoomRental.Models.Reservation", b =>
                {
                    b.Navigation("AdditionalServices");

                    b.Navigation("Documents");
                });

            modelBuilder.Entity("RoomRental.Models.Room", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("RoomRental.Models.User", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
