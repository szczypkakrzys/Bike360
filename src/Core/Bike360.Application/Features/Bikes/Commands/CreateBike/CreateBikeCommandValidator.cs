using FluentValidation;

namespace Bike360.Application.Features.Bikes.Commands.CreateBike;

public class CreateBikeCommandValidator : AbstractValidator<CreateBikeCommand>
{
    public CreateBikeCommandValidator()
    {
        RuleFor(p => p.Brand)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(p => p.Name)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");
    }
}
