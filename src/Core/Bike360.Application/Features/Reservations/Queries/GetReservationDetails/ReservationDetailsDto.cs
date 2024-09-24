namespace Bike360.Application.Features.Reservations.Queries.GetReservationDetails;

public class ReservationDetailsDto
{
    public DateTime DateTimeStart { get; set; }
    public DateTime DateTimeEnd { get; set; }
    public double Cost { get; set; }
    public string Status { get; set; }
    public string? Comments { get; set; }
    public CustomerDto CustomerData { get; set; }
    public IEnumerable<BikeDto> BikesData { get; set; }
}
