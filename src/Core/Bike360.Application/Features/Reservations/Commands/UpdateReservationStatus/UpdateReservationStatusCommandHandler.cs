using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Reservations.Commands.UpdateReservationStatus;

public class UpdateReservationStatusCommandHandler : IRequestHandler<UpdateReservationStatusCommand, Unit>
{
    private readonly IReservationRepository _reservationRepository;

    public UpdateReservationStatusCommandHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Unit> Handle(
        UpdateReservationStatusCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateReservationStatusCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid reservation data", validationResult);

        var reservationData = await _reservationRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Reservation), request.Id);

        if (!string.IsNullOrEmpty(request.Status))
            reservationData.Status = request.Status;

        await _reservationRepository.UpdateAsync(reservationData);

        return Unit.Value;
    }
}
