using MediatR;

namespace Bike360.Application.Features.Reservations.Commands.UpdateReservationStatus;

public class UpdateReservationStatusCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Status { get; set; }
}
