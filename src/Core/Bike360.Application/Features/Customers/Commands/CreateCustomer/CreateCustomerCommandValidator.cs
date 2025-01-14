using Bike360.Application.Contracts.Persistence;
using FluentValidation;

namespace Bike360.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        RuleFor(p => p.FirstName)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(p => p.LastName)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(p => p.EmailAddress)
            .NotEmpty()
                .WithMessage("{PropertyName} is required")
            .EmailAddress()
                .WithMessage("{PropertyValue} is not a valid Email")
            .MustAsync(CustomerEmailUnique)
                .WithMessage("Customer with given e-mail already exists");

        RuleFor(p => p.DateOfBirth)
           .NotEmpty()
               .WithMessage("{PropertyName} is required");

        RuleFor(p => p.PhoneNumber)
            .NotEmpty()
                .WithMessage("{PropertyName} is required")
            .Matches("^[^a-zA-Z]*$")
                .WithMessage("{PropertyName} cannot contain any letters");

        RuleFor(p => p.Address)
          .NotNull()
            .WithMessage("Address is required")
          .SetValidator(new CreateAddressDtoValidator());

        _customerRepository = customerRepository;
    }

    private Task<bool> CustomerEmailUnique(
        string email,
        CancellationToken token)
    {
        return _customerRepository.IsEmailUnique(email);
    }
}
