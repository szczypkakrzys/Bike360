using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetAllDivingSchoolCustomers;

public record GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>;
