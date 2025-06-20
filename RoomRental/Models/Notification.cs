using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomRental.Models;

public class Notification : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Type { get; set; }

    [Required]
    [StringLength(1000)]
    public string Content { get; set; }

    public DateTime SentAt { get; set; }
    public bool IsDelivered { get; set; }

    // Foreign Key
    public int UserId { get; set; }

    // Navigation property
    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    public void Send()
    {
        SentAt = DateTime.UtcNow;
        IsDelivered = true; // Simulation of sending
    }
}