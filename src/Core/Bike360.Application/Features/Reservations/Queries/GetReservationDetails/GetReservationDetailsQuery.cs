using MediatR;

namespace Bike360.Application.Features.Reservations.Queries.GetReservationDetails;

public record GetReservationDetailsQuery(int Id) : IRequest<ReservationDetailsDto>;