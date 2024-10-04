using MediatR;

namespace Bike360.Application.Features.Customers.Queries.GetCustomerDetails;

public record GetCustomerDetailsQuery(int Id) : IRequest<CustomerDetailsDto>;
