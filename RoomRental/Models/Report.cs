using System.ComponentModel.DataAnnotations;

namespace RoomRental.Models;

public class Report : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public ReportType Type { get; set; }

    [StringLength(500)]
    public string Criteria { get; set; }

    [StringLength(2000)]
    public string Content { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}