using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain.DivingSchool;
using MediatR;

namespace Bike360.Application.Features.Courses.Queries.GetCourseDetails;

public class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, CourseDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IDivingCourseRepository _courseRepository;

    public GetCourseDetailsQueryHandler(
        IMapper mapper,
        IDivingCourseRepository courseRepository)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
    }

    public async Task<CourseDetailsDto> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
    {
        var courseDetails = await _courseRepository.GetByIdAsync(request.Id) ??
                   throw new NotFoundException(nameof(DivingCourse), request.Id);

        var data = _mapper.Map<CourseDetailsDto>(courseDetails);

        return data;
    }
}
