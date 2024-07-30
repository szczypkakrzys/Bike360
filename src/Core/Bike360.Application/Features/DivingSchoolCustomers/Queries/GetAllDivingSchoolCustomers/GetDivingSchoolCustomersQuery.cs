using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetAllDivingSchoolCustomers;

public record GetDivingSchoolCustomersQuery : IRequest<IEnumerable<DivingSchoolCustomerDto>>;
