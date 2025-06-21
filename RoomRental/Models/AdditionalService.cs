using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomRental.Models;

public abstract class AdditionalService : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    public TimeSpan PreparationTime { get; set; }

    public int ReservationId { get; set; }

    [ForeignKey("ReservationId")]
    public virtual Reservation Reservation { get; set; }

    public abstract bool Validate();
}