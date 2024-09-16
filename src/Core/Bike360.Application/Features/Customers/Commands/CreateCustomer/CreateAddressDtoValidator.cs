using FluentValidation;

namespace Bike360.Application.Features.Customers.Commands.CreateCustomer;

public class CreateAddressDtoValidator : AbstractValidator<CreateAddressDto>
{
    public CreateAddressDtoValidator()
    {
        RuleFor(a => a.Country)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(a => a.Voivodeship)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(a => a.PostalCode)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(a => a.City)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(a => a.Street)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(a => a.HouseNumber)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");
    }
}
