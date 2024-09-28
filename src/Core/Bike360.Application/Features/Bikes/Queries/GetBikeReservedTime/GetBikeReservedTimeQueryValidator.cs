using Bike360.Application.Features.Bikes.Queries.GetBikeReservedDays;
using FluentValidation;

namespace Bike360.Application.Features.Bikes.Queries.GetBikeReservedTime;

public class GetBikeReservedTimeQueryValidator : AbstractValidator<GetBikeReservedTimeQuery>
{
    public GetBikeReservedTimeQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(p => p.TimeStart)
            .NotEmpty()
                .WithMessage("Start time is required")
            .LessThan(x => x.TimeEnd)
                .WithMessage("Start time must be before time end.");
    }
}
