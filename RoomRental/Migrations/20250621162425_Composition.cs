using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RoomRental.Migrations
{
    /// <inheritdoc />
    public partial class Composition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "Capacity", "CreatedAt", "Floor", "Name", "Status", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, 20, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Conference Room 101", 0, 0, null },
                    { 2, 1, 50, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "VIP Conference Suite", 0, 2, null },
                    { 3, 2, 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Auditorium", 0, 1, null },
                    { 4, 1, 15, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Meeting Room 102", 0, 0, null }
                });
        }
    }
}
