using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using MediatR;

namespace Bike360.Application.Features.Courses.Queries.GetAllCourses;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IEnumerable<CourseDto>>
{
    private readonly IMapper _mapper;
    private readonly IDivingCourseRepository _courseRepository;

    public GetCoursesQueryHandler(
        IMapper mapper,
        IDivingCourseRepository courseRepository)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<CourseDto>> Handle(
        GetCoursesQuery request,
        CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAsync();

        var data = _mapper.Map<IEnumerable<CourseDto>>(courses);

        return data;
    }
}
