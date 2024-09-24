using MediatR;

namespace Bike360.Application.Features.Reservations.Queries.GetCustomerReservations;

public record GetCustomerReservationsQuery(int CustomerId) : IRequest<IEnumerable<ReservationDto>>;
