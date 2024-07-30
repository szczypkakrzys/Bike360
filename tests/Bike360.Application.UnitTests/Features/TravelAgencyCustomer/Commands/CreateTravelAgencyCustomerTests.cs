using AutoMapper;
using CustomersManagement.Application.Contracts.Persistence;
using CustomersManagement.Application.Exceptions;
using CustomersManagement.Application.Features.Shared;
using CustomersManagement.Application.Features.TravelAgencyCustomers.Commands.CreateTravelAgencyCustomer;
using CustomersManagement.Domain.TravelAgency;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using NSubstitute;

namespace CustomersManagement.Application.UnitTests.Features.Customer.Commands;

public class CreateTravelAgencyCustomerTests
{
    private readonly ITravelAgencyCustomerRepository _customerRepository;
    private readonly CreateTravelAgencyCustomerCommandValidator _validator;
    private readonly IMapper _mapper;
    private readonly CreateTravelAgencyCustomerCommandHandler _handler;

    public CreateTravelAgencyCustomerTests()
    {
        _customerRepository = Substitute.For<ITravelAgencyCustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateTravelAgencyCustomerCommandHandler(_mapper, _customerRepository);
        _validator = new CreateTravelAgencyCustomerCommandValidator(_customerRepository);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreatedCustomerId()
    {
        // Arrange
        _customerRepository.IsCustomerUnique(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(true);
        var customerId = 1;
        var customerToCreate = new TravelAgencyCustomer { Id = customerId };

        var request = new CreateTravelAgencyCustomerCommand
        {
            FirstName = "Test",
            LastName = "Customer",
            EmailAddress = "test@customer.com",
            PhoneNumber = "1234567890",
            DateOfBirth = DateTime.UtcNow,
            Address = new CreateAddressDto()
        };

        _mapper.Map<TravelAgencyCustomer>(request).Returns(customerToCreate);
        _customerRepository.CreateAsync(customerToCreate).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(customerId);
    }

    [Fact]
    public async Task Validate_CustomerDataIsEmpty_ThrowsBadRequestExceptionAndShouldHaveValidationErrors()
    {
        // Arrange
        var request = new CreateTravelAgencyCustomerCommand();

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid Customer");

        await _customerRepository.DidNotReceive().CreateAsync(Arg.Any<TravelAgencyCustomer>());

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
        _customerRepository.IsCustomerUnique(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(true);

        var request = new CreateTravelAgencyCustomerCommand
        {
            FirstName = "Test",
            LastName = "Customer",
            EmailAddress = "testcustomercom",
            PhoneNumber = "1234567890",
            DateOfBirth = DateTime.UtcNow,
            Address = new CreateAddressDto()
        };

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid Customer");

        await _customerRepository.DidNotReceive().CreateAsync(Arg.Any<TravelAgencyCustomer>());

        result.ShouldHaveValidationErrorFor(request => request.EmailAddress)
            .WithErrorMessage($"{request.EmailAddress} is not a valid Email");
    }

    [Fact]
    public async Task Validate_CustomerAlreadyExists_ThrowsBadRequestExceptionAndShouldHaveValidationError()
    {
        // Arrange
        _customerRepository.IsCustomerUnique(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(false);

        var request = new CreateTravelAgencyCustomerCommand
        {
            FirstName = "Test",
            LastName = "Customer",
            EmailAddress = "test@customer.com",
            PhoneNumber = "1234567890",
            DateOfBirth = DateTime.UtcNow,
            Address = new CreateAddressDto()
        };

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid Customer");

        await _customerRepository.DidNotReceive().CreateAsync(Arg.Any<TravelAgencyCustomer>());

        result.ShouldHaveAnyValidationError()
            .WithErrorMessage("Given customer already exists");
    }
}
