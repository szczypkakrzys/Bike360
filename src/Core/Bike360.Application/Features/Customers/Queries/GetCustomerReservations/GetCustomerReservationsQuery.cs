using MediatR;

namespace Bike360.Application.Features.Customers.Queries.GetCustomerReservations;

public record GetCustomerReservationsQuery(int CustomerId) : IRequest<IEnumerable<ReservationDto>>;
