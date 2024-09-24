using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Reservations.Queries.GetReservationDetails;

public class GetReservationDetailsQueryHandler : IRequestHandler<GetReservationDetailsQuery, ReservationDetailsDto>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public GetReservationDetailsQueryHandler(
        IReservationRepository reservationRepository,
        IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<ReservationDetailsDto> Handle(
        GetReservationDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var reservationEntity = await _reservationRepository.GetReservationWithDetails(request.Id) ??
                    throw new NotFoundException(nameof(Reservation), request.Id);

        var reservationDetailsDto = _mapper.Map<ReservationDetailsDto>(reservationEntity);

        return reservationDetailsDto;
    }
}
