using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Reservations.Events;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Reservations.Commands.DeleteReservations;

public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, Unit>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ILogger<DeleteReservationCommandHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DeleteReservationCommandHandler(
        IReservationRepository reservationRepository,
        ILogger<DeleteReservationCommandHandler> logger,
        IMediator mediator,
        IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(
        DeleteReservationCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting reservation with ID = {ReservationId}", request.Id);

        var reservationToDelete = await _reservationRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundException(nameof(Reservation), request.Id);

        await _reservationRepository.DeleteAsync(reservationToDelete);

        await _mediator.Publish(_mapper.Map<ReservationDeletedEvent>(reservationToDelete));

        return Unit.Value;
    }
}
