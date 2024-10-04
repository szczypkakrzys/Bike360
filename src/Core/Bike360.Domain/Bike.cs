namespace Bike360.Domain;

public class Bike : BaseEntity
{
    public string Brand { get; set; }
    public string Type { get; set; }
    public string Model { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public double RentCostPerDay { get; set; }
    public string? FrameNumber { get; set; }
    public string? Description { get; set; }
    public ICollection<Reservation> Reservations { get; } = [];
    public ICollection<ReservationBike> ReservationBikes { get; } = [];
}
