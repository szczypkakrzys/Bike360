using MediatR;

namespace Bike360.Application.Features.Reservations.Commands.DeleteReservations;

public class DeleteReservationCommand : IRequest<Unit>
{
    public int Id { get; set; }
}