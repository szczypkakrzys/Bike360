using FluentValidation;

namespace Bike360.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(p => p.CustomerId)
           .NotEmpty()
               .WithMessage("{PropertyName} is required");

        RuleFor(p => p.DateTimeStart)
            .NotEmpty()
                .WithMessage("Start time is required")
            .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("Start time must be after current time.");

        RuleFor(p => p.NumberOfDays)
            .NotEmpty()
                .WithMessage("Number of days is required")
            .GreaterThan(0)
                .WithMessage("Number of days must be a positive value");

        RuleFor(p => p.BikesIds)
           .NotEmpty()
               .WithMessage("Bikes IDs are required");
    }
}
