using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomRental.Models;

public class Document : BaseEntity
{
    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }

    public DateTime IssueDate { get; set; }

    [StringLength(50)]
    public string PaymentMethod { get; set; }

    public DocumentStatus Status { get; set; }

    [Required]
    [StringLength(50)]
    public string DocumentNumber { get; set; }

    public DateTime DueDate { get; set; }

    // Foreign Key
    public int ReservationId { get; set; }

    // Navigation property
    [ForeignKey("ReservationId")]
    public virtual Reservation Reservation { get; set; }
}