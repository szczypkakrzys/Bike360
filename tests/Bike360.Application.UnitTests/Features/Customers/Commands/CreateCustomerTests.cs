﻿using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Customers.Commands.CreateCustomer;
using Bike360.Domain;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Customers.Commands;

public class CreateCustomerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly CreateCustomerCommandValidator _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCustomerCommandHandler> _logger;
    private readonly CreateCustomerCommandHandler _handler;

    public CreateCustomerTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateCustomerCommandHandler>>();
        _handler = new CreateCustomerCommandHandler(_mapper, _customerRepository, _logger);
        _validator = new CreateCustomerCommandValidator(_customerRepository);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreatedCustomerId()
    {
        // Arrange
        _customerRepository.IsEmailUnique(Arg.Any<string>()).Returns(true);
        var customerId = 1;
        var customerToCreate = new Customer { Id = customerId };

        var request = new CreateCustomerCommand
        {
            FirstName = "Test",
            LastName = "Customer",
            EmailAddress = "test@customer.com",
            PhoneNumber = "1234567890",
            DateOfBirth = DateTime.UtcNow,
            Address = new CreateAddressDto
            {
                Country = "Country",
                Voivodeship = "Voivodeship",
                PostalCode = "00-000",
                City = "City",
                Street = "Street",
                HouseNumber = "00/00"
            }
        };

        _mapper.Map<Customer>(request).Returns(customerToCreate);
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
        var request = new CreateCustomerCommand();

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid Customer");

        await _customerRepository.DidNotReceive().CreateAsync(Arg.Any<Customer>());

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
        _customerRepository.IsEmailUnique(Arg.Any<string>()).Returns(true);

        var request = new CreateCustomerCommand
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

        await _customerRepository.DidNotReceive().CreateAsync(Arg.Any<Customer>());

        result.ShouldHaveValidationErrorFor(request => request.EmailAddress)
            .WithErrorMessage($"{request.EmailAddress} is not a valid Email");
    }

    [Fact]
    public async Task Validate_EmailIsAlreadyRegistered_ThrowsBadRequestExceptionAndShouldHaveValidationError()
    {
        // Arrange
        _customerRepository.IsEmailUnique(Arg.Any<string>()).Returns(false);

        var request = new CreateCustomerCommand
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

        await _customerRepository.DidNotReceive().CreateAsync(Arg.Any<Customer>());

        result.ShouldHaveValidationErrorFor(request => request.EmailAddress)
            .WithErrorMessage("Customer with given e-mail already exists");
    }

    [Fact]
    public async Task Validate_PhoneNumberConatinsLetters_ThrowsBadRequestExceptionAndShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateCustomerCommand
        {
            FirstName = "Test",
            LastName = "Customer",
            EmailAddress = "test@customer.com",
            PhoneNumber = "abc1DEF2ghj",
            DateOfBirth = DateTime.UtcNow,
            Address = new CreateAddressDto()
        };

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid Customer");

        await _customerRepository.DidNotReceive().CreateAsync(Arg.Any<Customer>());

        result.ShouldHaveValidationErrorFor(request => request.PhoneNumber)
            .WithErrorMessage("Phone Number cannot contain any letters");
    }
}
