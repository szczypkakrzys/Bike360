using FluentValidation;

namespace Bike360.Application.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(p => p.TimeStart)
            .NotEmpty()
                .WithMessage("{PropertyName} is required")
            .LessThan(p => p.TimeEnd)
                .WithMessage("TimeStart must be before TimeEnd");

        RuleFor(p => p.TimeEnd)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(p => p.EntireCost)
            .NotEmpty()
                .WithMessage("{PropertyName} is required")
            .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be greater than or equal to 0");

        RuleFor(p => p.AdvancePaymentCost)
            .NotEmpty()
                .WithMessage("{PropertyName} is required")
            .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be greater than or equal to 0");

        RuleFor(p => p.EntireAmountPaymentDeadline)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(p => p.AdvancePaymentDeadline)
            .NotEmpty()
                .WithMessage("{PropertyName} is required")
            .LessThanOrEqualTo(p => p.EntireAmountPaymentDeadline)
                .WithMessage("AdvancePaymentDeadline must be before or the same time as EntireAmountPaymentDeadline")
            .LessThanOrEqualTo(p => p.TimeStart)
                .WithMessage("AdvancePaymentDeadline must be before or the same time as TimeStart");

        RuleFor(p => p.Status)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");
    }
}
