using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Reservations.Commands.UpdateReservationStatus;

public class UpdateReservationStatusCommandHandler : IRequestHandler<UpdateReservationStatusCommand, Unit>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ILogger<UpdateReservationStatusCommandHandler> _logger;

    public UpdateReservationStatusCommandHandler(
        IReservationRepository reservationRepository,
        ILogger<UpdateReservationStatusCommandHandler> logger)
    {
        _reservationRepository = reservationRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(
        UpdateReservationStatusCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating status of reservation with ID = {ReservationId} with {@UpdatedReservationStatus}", request.Id, request);

        var validator = new UpdateReservationStatusCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid reservation data", validationResult);

        var reservationData = await _reservationRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Reservation), request.Id);

        await _reservationRepository.UpdateAsync(reservationData);

        return Unit.Value;
    }
}
