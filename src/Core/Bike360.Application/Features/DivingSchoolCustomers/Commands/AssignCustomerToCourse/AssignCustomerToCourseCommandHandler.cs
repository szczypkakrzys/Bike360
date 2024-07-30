using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain.DivingSchool;
using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Commands.AssignCustomerToCourse;

public class AssignCustomerToCourseCommandHandler : IRequestHandler<AssignCustomerToCourseCommand, Unit>
{
    private readonly IDivingSchoolCustomerRepository _customerRepository;
    private readonly IDivingCourseRepository _courseRepository;
    private readonly ICustomersDivingCoursesRelationsRepository _relationsRepository;

    public AssignCustomerToCourseCommandHandler(
        IDivingSchoolCustomerRepository customerRepository,
        IDivingCourseRepository courseRepository,
        ICustomersDivingCoursesRelationsRepository relationsRepository)
    {
        _customerRepository = customerRepository;
        _courseRepository = courseRepository;
        _relationsRepository = relationsRepository;
    }

    public async Task<Unit> Handle(
        AssignCustomerToCourseCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new AssignCustomerToCourseCommandValidator(
                                       _relationsRepository,
                                       _customerRepository,
                                       _courseRepository);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid Relation", validationResult);

        var courseDetails = await _courseRepository.GetByIdAsync(request.CourseId);

        var relation = new CustomersDivingCoursesRelations
        {
            CustomerId = request.CustomerId,
            DivingCourseId = request.CourseId,
            EnrollmentDate = DateOnly.FromDateTime(DateTime.Now),
            AdvancedPaymentDate = null,
            EntireAmountPaymentDate = null,
            AdvancedPaymentLeftToPay = courseDetails.AdvancePaymentCost,
            EntireCostLeftToPay = courseDetails.EntireCost
        };

        await _relationsRepository.CreateAsync(relation);

        return Unit.Value;
    }
}
