using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Reservations.Commands.DeleteReservations;

public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, Unit>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ILogger<DeleteReservationCommandHandler> _logger;

    public DeleteReservationCommandHandler(
        IReservationRepository reservationRepository,
        ILogger<DeleteReservationCommandHandler> logger)
    {
        _reservationRepository = reservationRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(
        DeleteReservationCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting reservation with ID = {ReservationId}", request.Id);

        var reservationToDelete = await _reservationRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundException(nameof(Reservation), request.Id);

        await _reservationRepository.DeleteAsync(reservationToDelete);

        return Unit.Value;
    }
}
