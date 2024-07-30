using MediatR;

namespace Bike360.Application.Features.Courses.Queries.GetAllCourseParticipants;

public record GetCourseParticipantsQuery(int Id) : IRequest<IEnumerable<CourseParticipantDto>>;
