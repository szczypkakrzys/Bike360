using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using MediatR;

namespace Bike360.Application.Features.Courses.Queries.GetAllCourseParticipants;

public class GetCourseParticipantsQueryHandler : IRequestHandler<GetCourseParticipantsQuery, IEnumerable<CourseParticipantDto>>
{
    private readonly IDivingCourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public GetCourseParticipantsQueryHandler(
        IDivingCourseRepository courseRepository,
        IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseParticipantDto>> Handle(
        GetCourseParticipantsQuery request,
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdWithParticipants(request.Id) ??
                       throw new NotFoundException("Course", request.Id);

        var result = _mapper.Map<IEnumerable<CourseParticipantDto>>(course.Participants);

        return result;
    }
}
