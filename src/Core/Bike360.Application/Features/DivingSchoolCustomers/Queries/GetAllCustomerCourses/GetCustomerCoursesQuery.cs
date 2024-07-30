using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetAllCustomerCourses;

public record GetCustomerCoursesQuery(int Id) : IRequest<IEnumerable<CustomerCourseDto>>;
