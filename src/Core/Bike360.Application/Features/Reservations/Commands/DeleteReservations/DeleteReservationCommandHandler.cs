using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Reservations.Commands.DeleteReservations;

public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, Unit>
{
    private readonly IReservationRepository _reservationRepository;

    public DeleteReservationCommandHandler(IReservationRepository reservationRepository) =>
        _reservationRepository = reservationRepository;

    public async Task<Unit> Handle(
        DeleteReservationCommand request,
        CancellationToken cancellationToken)
    {
        var reservationToDelete = await _reservationRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundException(nameof(Reservation), request.Id);

        await _reservationRepository.DeleteAsync(reservationToDelete);

        return Unit.Value;
    }
}
