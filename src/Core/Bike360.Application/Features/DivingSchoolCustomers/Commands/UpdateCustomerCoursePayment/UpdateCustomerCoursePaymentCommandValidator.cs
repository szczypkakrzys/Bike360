using FluentValidation;

namespace Bike360.Application.Features.DivingSchoolCustomers.Commands.UpdateCustomerCoursePayment;

public class UpdateCustomerCoursePaymentCommandValidator : AbstractValidator<UpdateCustomerCoursePaymentCommand>
{
    public UpdateCustomerCoursePaymentCommandValidator(double entireCostLeftToPay)
    {
        RuleFor(p => p.CustomerId)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(p => p.CourseId)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(p => p.PaymentAmount)
            .NotEmpty()
                .WithMessage("{PropertyName} is required")
            .LessThanOrEqualTo(entireCostLeftToPay)
               .WithMessage("{PropertyName} can't be greater than amount left to pay.");
    }
}
