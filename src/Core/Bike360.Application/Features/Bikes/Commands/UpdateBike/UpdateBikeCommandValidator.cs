using FluentValidation;

namespace Bike360.Application.Features.Bikes.Commands.UpdateBike;

public class UpdateBikeCommandValidator : AbstractValidator<UpdateBikeCommand>
{
    public UpdateBikeCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(p => p.Brand)
           .NotEmpty()
               .WithMessage("{PropertyName} is required");

        RuleFor(p => p.Type)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(p => p.Model)
           .NotEmpty()
               .WithMessage("{PropertyName} is required");

        RuleFor(p => p.Size)
           .NotEmpty()
               .WithMessage("{PropertyName} is required");

        RuleFor(p => p.Color)
           .NotEmpty()
               .WithMessage("{PropertyName} is required");

        RuleFor(p => p.RentCostPerDay)
            .NotEmpty()
                .WithMessage("Rent cost is required")
            .GreaterThan(0)
                .WithMessage("Rent cost must be greater than 0");
    }
}
