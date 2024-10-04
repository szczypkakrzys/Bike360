namespace Bike360.Domain;

public class Reservation : BaseEntity
{
    public DateTime DateTimeStartInUtc { get; set; }
    public DateTime DateTimeEndInUtc { get; set; }
    public double Cost { get; set; }
    public string Status { get; set; }
    public string? Comments { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<Bike> Bikes { get; } = [];
    public ICollection<ReservationBike> ReservationBikes { get; } = [];
}
