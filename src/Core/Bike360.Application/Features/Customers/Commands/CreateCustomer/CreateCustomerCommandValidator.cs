using Bike360.Application.Contracts.Persistence;
using FluentValidation;

namespace Bike360.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        //TODO
        //- add proper validation for e-mail and phone number to keep working properly 
        //- add proper validation for address form :)))

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
                .WithMessage("{PropertyValue} is not a valid Email");

        RuleFor(q => q)
            .MustAsync(CustomerDataUnique)
                .WithMessage("Given customer already exists");

        RuleFor(p => p.DateOfBirth)
           .NotEmpty()
               .WithMessage("{PropertyName} is required");

        RuleFor(p => p.PhoneNumber)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(p => p.Address)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");
        //TODO
        //add validation for all address fields
        _customerRepository = customerRepository;
    }

    private Task<bool> CustomerDataUnique(
        CreateCustomerCommand command,
        CancellationToken token)
    {
        return _customerRepository.IsCustomerUnique(
            command.FirstName,
            command.LastName,
            command.EmailAddress);
    }
}
