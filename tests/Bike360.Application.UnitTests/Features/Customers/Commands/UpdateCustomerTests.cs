using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Customers.Commands.UpdateCustomer;
using Bike360.Application.Features.Shared;
using Bike360.Domain;
using FluentAssertions;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Customers.Commands;

public class UpdateCustomerTests
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;
    private readonly UpdateCustomerCommandValidator _validator;
    private readonly UpdateCustomerCommandHandler _handler;

    public UpdateCustomerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _customerRepository = Substitute.For<ICustomerRepository>();
        _logger = Substitute.For<ILogger<UpdateCustomerCommandHandler>>();
        _validator = new UpdateCustomerCommandValidator();
        _handler = new UpdateCustomerCommandHandler(_mapper, _customerRepository, _logger);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsUnitValue()
    {
        // Arrange
        var customerId = 1;
        var request = new UpdateCustomerCommand
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Customer",
            EmailAddress = "test@customer.com",
            PhoneNumber = "1234567890",
            DateOfBirth = DateTime.UtcNow,
            Address = new AddressDto
            {
                Country = "Country",
                Voivodeship = "Voivodeship",
                PostalCode = "00-000",
                City = "City",
                Street = "Street",
                HouseNumber = "00/00"
            }
        };

        var customerToUpdate = new Customer();

        _customerRepository.IsCustomerUnique(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(true);
        _customerRepository.GetByIdAsync(customerId).Returns(customerToUpdate);
        _customerRepository.UpdateAsync(customerToUpdate).Returns(Task.CompletedTask);
        _mapper.Map<Customer>(request).Returns(customerToUpdate);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task Handle_WithNonexistentCustomerId_ThrowsNotFoundExceptionAndShouldHaveIdValidationError()
    {
        // Arrange
        var customerId = 1;
        var request = new UpdateCustomerCommand
        {
            Id = customerId,
            FirstName = "Test",
            LastName = "Customer",
            EmailAddress = "test@customer.com",
            PhoneNumber = "1234567890",
            DateOfBirth = DateTime.UtcNow,
            Address = new AddressDto
            {
                Country = "Country",
                Voivodeship = "Voivodeship",
                PostalCode = "00-000",
                City = "City",
                Street = "Street",
                HouseNumber = "00/00"
            }
        };

        _customerRepository.GetByIdAsync(customerId).Returns(default(Customer));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
           .WithMessage($"{nameof(Customer)} with ID = {customerId} was not found");

        await _customerRepository.DidNotReceive().UpdateAsync(Arg.Any<Customer>());
    }

    [Fact]
    public async Task Validate_CustomerDataIsEmpty_ThrowsBadRequestExceptionAndShouldHaveValidationErrors()
    {
        // Arrange
        var request = new UpdateCustomerCommand();

        // Act 
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
           .WithMessage("Invalid customer data");

        await _customerRepository.DidNotReceive().UpdateAsync(Arg.Any<Customer>());

        result.ShouldHaveValidationErrorFor(request => request.Id)
            .WithErrorMessage("Id is required");
        result.ShouldHaveValidationErrorFor(request => request.FirstName)
            .WithErrorMessage("First Name is required");
        result.ShouldHaveValidationErrorFor(request => request.LastName)
            .WithErrorMessage("Last Name is required");
        result.ShouldHaveValidationErrorFor(request => request.EmailAddress)
            .WithErrorMessage("Email Address is required");
        result.ShouldHaveValidationErrorFor(request => request.DateOfBirth)
           .WithErrorMessage("Date Of Birth is required");
        result.ShouldHaveValidationErrorFor(request => request.PhoneNumber)
           .WithErrorMessage("Phone Number is required");
        result.ShouldHaveValidationErrorFor(request => request.Address)
            .WithErrorMessage("Address is required");
    }

    [Fact]
    public async Task Validate_EmailAddressHasIncorrectFormat_ThrowsBadRequestExceptionAndShouldHaveEmailValidationError()
    {
        // Arrange
        var customerId = 1;
        var request = new UpdateCustomerCommand
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Customer",
            EmailAddress = "testcustomercom",
            PhoneNumber = "1234567890",
            DateOfBirth = DateTime.UtcNow,
            Address = new AddressDto()
        };

        var customerToUpdate = new Customer();

        _customerRepository.IsCustomerUnique(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(true);
        _customerRepository.GetByIdAsync(customerId).Returns(customerToUpdate);

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
           .WithMessage("Invalid customer data");

        await _customerRepository.DidNotReceive().UpdateAsync(Arg.Any<Customer>());

        result.ShouldHaveValidationErrorFor(request => request.EmailAddress)
            .WithErrorMessage($"{request.EmailAddress} is not a valid Email");
    }
}
