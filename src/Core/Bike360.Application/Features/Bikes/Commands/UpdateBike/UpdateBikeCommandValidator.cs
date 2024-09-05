using Bike360.Application.Contracts.Persistence;
using FluentValidation;

namespace Bike360.Application.Features.Bikes.Commands.UpdateBike;

public class UpdateBikeCommandValidator : AbstractValidator<UpdateBikeCommand>
{
    private readonly IBikeRepository _bikeRepository;

    public UpdateBikeCommandValidator(IBikeRepository bikeRepository)
    {
        RuleFor(p => p.Id)
                  .NotEmpty()
                      .WithMessage("{PropertyName} is required")
                  .MustAsync(BikeMustExist)
                      .WithMessage("Couldn't find bike with Id = {PropertyValue}");

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

        _bikeRepository = bikeRepository;
    }

    private async Task<bool> BikeMustExist(
    int id,
      CancellationToken token)
    {
        var bike = await _bikeRepository.GetByIdAsync(id);
        return bike != null;
    }
}
