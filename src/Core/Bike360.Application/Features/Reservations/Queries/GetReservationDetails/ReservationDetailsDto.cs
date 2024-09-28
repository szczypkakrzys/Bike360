namespace Bike360.Application.Features.Reservations.Queries.GetReservationDetails;

public class ReservationDetailsDto
{
    public DateTime DateTimeStartInUtc { get; set; }
    public DateTime DateTimeEndInUtc { get; set; }
    public double Cost { get; set; }
    public string Status { get; set; }
    public string? Comments { get; set; }
    public CustomerDto CustomerData { get; set; }
    public IEnumerable<BikeDto> BikesData { get; set; }
}
