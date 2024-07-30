using AutoMapper;
using CustomersManagement.Application.Contracts.Persistence;
using CustomersManagement.Application.Exceptions;
using CustomersManagement.Application.Features.Shared;
using CustomersManagement.Application.Features.TravelAgencyCustomers.Commands.UpdateTravelAgencyCustomer;
using CustomersManagement.Domain.TravelAgency;
using FluentAssertions;
using FluentValidation.TestHelper;
using MediatR;
using NSubstitute;

namespace CustomersManagement.Application.UnitTests.Features.Customer.Commands;

public class UpdateTravelAgencyCustomerTests
{
    private readonly IMapper _mapper;
    private readonly ITravelAgencyCustomerRepository _customerRepository;
    private readonly UpdateTravelAgencyCustomerCommandValidator _validator;
    private readonly UpdateTravelAgencyCustomerCommandHandler _handler;

    public UpdateTravelAgencyCustomerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _customerRepository = Substitute.For<ITravelAgencyCustomerRepository>();
        _validator = new UpdateTravelAgencyCustomerCommandValidator(_customerRepository);
        _handler = new UpdateTravelAgencyCustomerCommandHandler(_mapper, _customerRepository);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsUnitValue()
    {
        // Arrange
        var customerId = 1;
        var request = new UpdateTravelAgencyCustomerCommand
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Customer",
            EmailAddress = "test@customer.com",
            PhoneNumber = "1234567890",
            DateOfBirth = DateTime.UtcNow,
            Address = new AddressDto()
        };

        var customerToUpdate = new TravelAgencyCustomer();

        _customerRepository.IsCustomerUnique(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(true);
        _customerRepository.GetByIdAsync(customerId).Returns(customerToUpdate);
        _customerRepository.UpdateAsync(customerToUpdate).Returns(Task.CompletedTask);
        _mapper.Map<TravelAgencyCustomer>(request).Returns(customerToUpdate);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task Validate_WithNonexistentCustomerId_ThrowsBadRequestExceptionAndShouldHaveIdValidationError()
    {
        // Arrange
        var customerId = 1;
        var request = new UpdateTravelAgencyCustomerCommand
        {
            Id = customerId,
            FirstName = "Test",
            LastName = "Customer",
            EmailAddress = "test@customer.com",
            PhoneNumber = "1234567890",
            DateOfBirth = DateTime.UtcNow,
            Address = new AddressDto()
        };

        var customerToUpdate = new TravelAgencyCustomer();

        _customerRepository.GetByIdAsync(customerId).Returns(default(TravelAgencyCustomer));

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
           .WithMessage("Invalid customer data");

        await _customerRepository.DidNotReceive().UpdateAsync(Arg.Any<TravelAgencyCustomer>());

        result.ShouldHaveValidationErrorFor(request => request.Id)
            .WithErrorMessage($"Couldn't find customer with Id = {request.Id}");
    }

    [Fact]
    public async Task Validate_CustomerDataIsEmpty_ThrowsBadRequestExceptionAndShouldHaveValidationErrors()
    {
        // Arrange
        var request = new UpdateTravelAgencyCustomerCommand();

        // Act 
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
           .WithMessage("Invalid customer data");

        await _customerRepository.DidNotReceive().UpdateAsync(Arg.Any<TravelAgencyCustomer>());

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
        var request = new UpdateTravelAgencyCustomerCommand
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Customer",
            EmailAddress = "testcustomercom",
            PhoneNumber = "1234567890",
            DateOfBirth = DateTime.UtcNow,
            Address = new AddressDto()
        };

        var customerToUpdate = new TravelAgencyCustomer();

        _customerRepository.IsCustomerUnique(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(true);
        _customerRepository.GetByIdAsync(customerId).Returns(customerToUpdate);

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
           .WithMessage("Invalid customer data");

        await _customerRepository.DidNotReceive().UpdateAsync(Arg.Any<TravelAgencyCustomer>());

        result.ShouldHaveValidationErrorFor(request => request.EmailAddress)
            .WithErrorMessage($"{request.EmailAddress} is not a valid Email");
    }
}
