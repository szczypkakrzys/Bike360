namespace Bike360.Application.Features.Reservations.Queries.GetCustomerReservations;

public class ReservationDto
{
    public int Id { get; set; }
    public DateTime DateTimeStartInUtc { get; set; }
    public DateTime DateTimeEndInUtc { get; set; }
    public double Cost { get; set; }
    public string? Comments { get; set; }
    public string Status { get; set; }
}
