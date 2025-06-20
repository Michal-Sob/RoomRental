using System.ComponentModel.DataAnnotations.Schema;

namespace RoomRental.Models;

public class Reservation : BaseEntity
{
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public ReservationStatus Status { get; set; }

    public int UserId { get; set; }
    public int RoomId { get; set; }
    public int? MeetingId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    
    [ForeignKey("RoomId")]
    public virtual Room Room { get; set; }
    
    [ForeignKey("MeetingId")]
    public virtual Meeting Meeting { get; set; }
    
    public virtual ICollection<AdditionalService> AdditionalServices { get; set; } = new List<AdditionalService>();
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public void FinalizeReservation()
    {
        if (Status == ReservationStatus.Pending)
        {
            Status = ReservationStatus.Confirmed;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void AssignEquipment(List<Equipment> equipment)
    {
    }

    public void AssignCatering(Catering catering)
    {
        AdditionalServices.Add(catering);
    }

    public decimal CalculateTotalAmount()
    {
        var duration = EndTime - StartTime;
        decimal roomCost = Room.CalculateCost(duration);
        decimal servicesCost = AdditionalServices.Sum(s => s.Price);
        return roomCost + servicesCost;
    }

    public TimeSpan Duration => EndTime - StartTime;
}