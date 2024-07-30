using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain.DivingSchool;
using MediatR;

namespace Bike360.Application.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IDivingCourseRepository _courseRepository;

    public UpdateCourseCommandHandler(
        IMapper mapper,
        IDivingCourseRepository courseRepository)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
    }

    public async Task<Unit> Handle(
        UpdateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateCourseCommandValidator(_courseRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid Course data", validationResult);

        var courseToUpdate = _mapper.Map<DivingCourse>(request);

        await _courseRepository.UpdateAsync(courseToUpdate);

        return Unit.Value;
    }
}
