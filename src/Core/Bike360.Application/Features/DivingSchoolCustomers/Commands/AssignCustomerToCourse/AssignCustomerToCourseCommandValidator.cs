using Bike360.Application.Contracts.Persistence;
using FluentValidation;

namespace Bike360.Application.Features.DivingSchoolCustomers.Commands.AssignCustomerToCourse;

public class AssignCustomerToCourseCommandValidator : AbstractValidator<AssignCustomerToCourseCommand>
{
    private readonly ICustomersDivingCoursesRelationsRepository _relationsRepository;
    private readonly IDivingSchoolCustomerRepository _customerRepository;
    private readonly IDivingCourseRepository _courseRepository;

    public AssignCustomerToCourseCommandValidator(
        ICustomersDivingCoursesRelationsRepository relationsRepository,
        IDivingSchoolCustomerRepository customerRepository,
        IDivingCourseRepository courseRepository)
    {
        RuleFor(q => q)
            .MustAsync(CustomerIsNotAlreadyAssingedToGivenCourse)
                .WithMessage("Given customer is already assigned to this course");

        RuleFor(p => p.CustomerId)
            .NotEmpty()
               .WithMessage("{PropertyName} is required")
            .MustAsync(CustomerMustExist)
                .WithMessage("Couldn't find customer with Id = {PropertyValue}");


        RuleFor(p => p.CourseId)
            .NotEmpty()
                .WithMessage("{PropertyName} is required")
            .MustAsync(CourseMustExist)
                .WithMessage("Couldn't find course with Id = {PropertyValue}");

        _relationsRepository = relationsRepository;
        _customerRepository = customerRepository;
        _courseRepository = courseRepository;
    }

    private Task<bool> CustomerIsNotAlreadyAssingedToGivenCourse(
        AssignCustomerToCourseCommand command,
        CancellationToken token)
    {
        return _relationsRepository.IsNotAlreadyAssigned(
            command.CustomerId,
            command.CourseId);
    }

    private async Task<bool> CustomerMustExist(
        int id,
        CancellationToken token)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return customer != null;
    }

    private async Task<bool> CourseMustExist(
        int id,
        CancellationToken token)
    {
        var tour = await _courseRepository.GetByIdAsync(id);
        return tour != null;
    }
}
