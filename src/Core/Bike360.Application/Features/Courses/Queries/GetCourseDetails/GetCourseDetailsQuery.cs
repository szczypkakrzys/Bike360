using MediatR;

namespace Bike360.Application.Features.Courses.Queries.GetCourseDetails;

public record GetCourseDetailsQuery(int Id) : IRequest<CourseDetailsDto>;
