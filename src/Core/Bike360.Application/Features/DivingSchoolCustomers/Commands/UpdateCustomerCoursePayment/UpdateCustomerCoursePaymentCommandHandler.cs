using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Services;
using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Commands.UpdateCustomerCoursePayment;

public class UpdateCustomerCoursePaymentCommandHandler : IRequestHandler<UpdateCustomerCoursePaymentCommand, Unit>
{
    private readonly ICustomersDivingCoursesRelationsRepository _relationsRepository;

    public UpdateCustomerCoursePaymentCommandHandler(ICustomersDivingCoursesRelationsRepository relationsRepository) => _relationsRepository = relationsRepository;

    public async Task<Unit> Handle(
        UpdateCustomerCoursePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var details = await _relationsRepository.GetCustomerCourseDetails(request.CustomerId, request.CourseId) ??
                  throw new NotFoundException($"Relation with customerId: {request.CustomerId} and courseId:", request.CourseId);

        if (details.EntireCostLeftToPay == 0)
            throw new BadRequestException("Course cost is already paid");

        var validator = new UpdateCustomerCoursePaymentCommandValidator(details.EntireCostLeftToPay);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid payment data", validationResult);

        var afterPaymentDetails = PaymentService.RelationAfterPayment(details, request.PaymentAmount);

        details.EntireCostLeftToPay = afterPaymentDetails.EntireCostLeftToPay;
        details.AdvancedPaymentLeftToPay = afterPaymentDetails.AdvancedPaymentLeftToPay;
        details.EntireAmountPaymentDate = afterPaymentDetails.EntireAmountPaymentDate;
        details.AdvancedPaymentDate = afterPaymentDetails.AdvancedPaymentDate;

        await _relationsRepository.UpdateAsync(details);

        return Unit.Value;
    }
}
