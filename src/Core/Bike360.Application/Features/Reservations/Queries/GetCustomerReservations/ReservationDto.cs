namespace Bike360.Application.Features.Reservations.Queries.GetCustomerReservations;

public class ReservationDto
{
    public DateTime DateTimeStart { get; set; }
    public DateTime DateTimeEnd { get; set; }
    public double Cost { get; set; }
    public string? Comments { get; set; }
    public string Status { get; set; }
}
