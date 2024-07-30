using Bike360.Application.Contracts.Persistence;
using FluentValidation;

namespace Bike360.Application.Features.DivingSchoolCustomers.Commands.CreateDivingSchoolCustomer;

public class CreateDivingSchoolCustomerCommandValidator : AbstractValidator<CreateDivingSchoolCustomerCommand>
{
    private readonly IDivingSchoolCustomerRepository _customerRepository;

    public CreateDivingSchoolCustomerCommandValidator(IDivingSchoolCustomerRepository customerRepository)
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

        RuleFor(p => p.DivingCertificationLevel)
           .NotEmpty()
               .WithMessage("{PropertyName} is required");

        RuleFor(p => p.Address)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        _customerRepository = customerRepository;
    }

    private Task<bool> CustomerDataUnique(
        CreateDivingSchoolCustomerCommand command,
        CancellationToken token)
    {
        return _customerRepository.IsCustomerUnique(
            command.FirstName,
            command.LastName,
            command.EmailAddress);
    }
}
