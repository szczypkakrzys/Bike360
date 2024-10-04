namespace Bike360.Application.Features.Reservations.Queries.GetReservationDetails;

public class BikeDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Type { get; set; }
    public string Size { get; set; }
    public double RentCostPerDay { get; set; }
}
