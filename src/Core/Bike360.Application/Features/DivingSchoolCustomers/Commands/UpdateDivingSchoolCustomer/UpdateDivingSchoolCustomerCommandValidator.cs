using Bike360.Application.Contracts.Persistence;
using FluentValidation;

namespace Bike360.Application.Features.DivingSchoolCustomers.Commands.UpdateDivingSchoolCustomer;

public class UpdateDivingSchoolCustomerCommandValidator : AbstractValidator<UpdateDivingSchoolCustomerCommand>
{
    private readonly IDivingSchoolCustomerRepository _customerRepository;

    public UpdateDivingSchoolCustomerCommandValidator(IDivingSchoolCustomerRepository customerRepository)
    {
        RuleFor(p => p.Id)
            .NotEmpty()
                .WithMessage("{PropertyName} is required")
            .MustAsync(CustomerMustExist)
                .WithMessage("Couldn't find customer with Id = {PropertyValue}");

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

    private async Task<bool> CustomerMustExist(
        int id,
        CancellationToken token)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return customer != null;
    }
}
