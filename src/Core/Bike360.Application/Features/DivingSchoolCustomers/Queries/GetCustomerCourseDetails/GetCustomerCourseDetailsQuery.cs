using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetCustomerCourseDetails;

public record GetCustomerCourseDetailsQuery(int customerId, int courseId) : IRequest<CustomerCourseDetailsDto>;
