namespace Bike360.Domain;

public class ReservationBike : BaseEntity
{
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;

    public int BikeId { get; set; }
    public Bike Bike { get; set; } = null!;
}
