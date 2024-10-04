using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Reservations.Queries.GetReservationDetails;

public class GetReservationDetailsQueryHandler : IRequestHandler<GetReservationDetailsQuery, ReservationDetailsDto>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetReservationDetailsQueryHandler> _logger;

    public GetReservationDetailsQueryHandler(
        IReservationRepository reservationRepository,
        IMapper mapper,
        ILogger<GetReservationDetailsQueryHandler> logger)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ReservationDetailsDto> Handle(
        GetReservationDetailsQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reservation with ID = {ReservationId}", request.Id);

        var reservationEntity = await _reservationRepository.GetReservationWithDetails(request.Id) ??
                    throw new NotFoundException(nameof(Reservation), request.Id);

        var reservationDetailsDto = _mapper.Map<ReservationDetailsDto>(reservationEntity);

        return reservationDetailsDto;
    }
}
