using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetDivingSchoolCustomerDetails;

public record GetCustomerDetailsQuery(int Id) : IRequest<CustomerDetailsDto>;
