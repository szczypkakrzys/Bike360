using MediatR;

namespace Bike360.Application.Features.Courses.Queries.GetAllCourses;

public record GetCoursesQuery : IRequest<IEnumerable<CourseDto>>;
