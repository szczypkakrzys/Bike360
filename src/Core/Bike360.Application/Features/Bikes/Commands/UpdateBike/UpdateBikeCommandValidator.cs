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

        RuleFor(p => p.Name)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

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
