using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetDivingSchoolCustomerDetails;

public record GetDivingSchoolCustomerDetailsQuery(int Id) : IRequest<DivingSchoolCustomerDetailsDto>;
