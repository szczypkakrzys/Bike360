using MediatR;

namespace Bike360.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommand : IRequest<int>
{
    public DateTime DateTimeStartInUtc { get; set; }
    public int NumberOfDays { get; set; }
    public string? Comments { get; set; }
    public int CustomerId { get; set; }
    public IEnumerable<int> BikesIds { get; set; }
}
