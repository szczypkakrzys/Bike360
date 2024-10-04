using MediatR;

namespace Bike360.Application.Features.Customers.Queries.GetAllCustomers;

public record GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>;
