using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Reservations.Commands.CreateReservation;
using Bike360.Application.Features.Reservations.Results;
using Bike360.Application.Features.Reservations.Services;
using Bike360.Domain;
using FluentAssertions;
using FluentValidation.TestHelper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Bike360.Application.UnitTests.Features.Reservations.Commands;

public class CreateReservationTests
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IBikeRepository _bikeRepository;
    private readonly IReservationService _reservationService;
    private readonly CreateReservationCommandHandler _handler;
    private readonly CreateReservationCommandValidator _validator;

    public CreateReservationTests()
    {
        _mapper = Substitute.For<IMapper>();
        _customerRepository = Substitute.For<ICustomerRepository>();
        _reservationRepository = Substitute.For<IReservationRepository>();
        _bikeRepository = Substitute.For<IBikeRepository>();
        _reservationService = Substitute.For<IReservationService>();
        _handler = new CreateReservationCommandHandler(_reservationRepository, _bikeRepository, _customerRepository, _mapper, _reservationService);
        _validator = new CreateReservationCommandValidator();
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreatedReservationId()
    {
        // Arrange
        var reservationId = 1;
        var reservationToCreate = new Reservation { Id = reservationId };

        var request = new CreateReservationCommand
        {
            DateTimeStart = DateTime.Now.AddDays(1),
            NumberOfDays = 1,
            Comments = "Reservation comments",
            CustomerId = 1,
            BikesIds = new[] { 1, 2, 3 }
        };

        var customer = new Customer { Id = request.CustomerId };
        _customerRepository.GetByIdAsync(request.CustomerId).Returns(customer);

        var reservationBikes = new List<Bike> { new(), new(), new() };
        _bikeRepository.GetByIdsAsync(request.BikesIds).Returns(reservationBikes);

        var successResult = new AvailabilityResult { AreAvailable = true, ErrorMessage = string.Empty };
        _reservationService.CheckBikesAvailability(request.BikesIds, request.DateTimeStart, request.DateTimeStart.AddDays(request.NumberOfDays)).Returns(successResult);

        _mapper.Map<Reservation>(request).Returns(reservationToCreate);
        _reservationRepository.CreateAsync(reservationToCreate).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(reservationId);
    }

    [Fact]
    public async Task Handle_BikesDoNotExist_ThrowsNotFoundExceptionAndShouldHaveBikesValidationError()
    {
        // Arrange
        var request = new CreateReservationCommand
        {
            DateTimeStart = DateTime.Now.AddDays(1),
            NumberOfDays = 1,
            Comments = "Reservation comments",
            CustomerId = 1,
            BikesIds = new[] { 1, 2, 3 }
        };

        var customer = new Customer { Id = request.CustomerId };
        _customerRepository.GetByIdAsync(request.CustomerId).Returns(customer);

        var foundBikes = new List<Bike>
        {
            new() { Id = 1 }
        };
        _bikeRepository.GetByIdsAsync(request.BikesIds).Returns(foundBikes);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Invalid reservation data. Bikes with following IDs were not found: 2, 3");
    }

    [Fact]
    public async Task Handle_BikeIsNotAvailableInThisTimePeriod_ThrowsBadRequestExceptionAndShouldHaveBikeValidationError()
    {
        // Arrange
        var request = new CreateReservationCommand
        {
            DateTimeStart = DateTime.Now.AddDays(1),
            NumberOfDays = 1,
            Comments = "Reservation comments",
            CustomerId = 1,
            BikesIds = new[] { 1, 2, 3 }
        };

        var customer = new Customer { Id = request.CustomerId };
        _customerRepository.GetByIdAsync(request.CustomerId).Returns(customer);

        var reservationBikes = new List<Bike> { new(), new(), new() };
        _bikeRepository.GetByIdsAsync(request.BikesIds).Returns(reservationBikes);

        var availabilityResult = new AvailabilityResult { AreAvailable = false, ErrorMessage = "Bikes with IDs: ... are not available in given period" };
        _reservationService.CheckBikesAvailability(request.BikesIds, request.DateTimeStart, request.DateTimeStart.AddDays(request.NumberOfDays)).Returns(availabilityResult);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage(availabilityResult.ErrorMessage);
    }

    [Fact]
    public async Task Handle_CustomerDoesNotExist_ThrowsNotFoundExceptionAndShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateReservationCommand
        {
            DateTimeStart = DateTime.Now.AddDays(1),
            NumberOfDays = 1,
            Comments = "Reservation comments",
            CustomerId = 1,
            BikesIds = new[] { 1, 2, 3 }
        };

        _customerRepository.GetByIdAsync(request.CustomerId).ReturnsNull();

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Customer with ID = {request.CustomerId} was not found");
    }

    [Fact]
    public async Task Validate_ReservationDataIsEmpty_ThrowsBadRequestExceptionAndShouldHaveValidationErrors()
    {
        // Arrange
        var request = new CreateReservationCommand();

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
           .WithMessage("Invalid Reservation");

        await _reservationRepository.DidNotReceive().CreateAsync(Arg.Any<Reservation>());

        result.ShouldHaveValidationErrorFor(request => request.CustomerId)
            .WithErrorMessage("Customer Id is required");
        result.ShouldHaveValidationErrorFor(request => request.DateTimeStart)
           .WithErrorMessage("Start time is required");
        result.ShouldHaveValidationErrorFor(request => request.NumberOfDays)
           .WithErrorMessage("Number of days is required");
        result.ShouldHaveValidationErrorFor(request => request.BikesIds)
           .WithErrorMessage("Bikes IDs are required");
    }

    [Fact]
    public async Task Validate_NumberOfDaysIsNegative_ThrowsBadRequestExceptionAndShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateReservationCommand
        {
            DateTimeStart = DateTime.Now.AddDays(1),
            NumberOfDays = -4,
            Comments = "Reservation comments",
            CustomerId = 1,
            BikesIds = new[] { 1, 2, 3 }
        };

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
           .WithMessage("Invalid Reservation");

        await _reservationRepository.DidNotReceive().CreateAsync(Arg.Any<Reservation>());

        result.ShouldHaveValidationErrorFor(request => request.NumberOfDays)
            .WithErrorMessage("Number of days must be a positive value");
    }

    [Fact]
    public async Task Validate_DateStartIsBeforeCurrentTime_ThrowsBadRequestExceptionAndShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateReservationCommand
        {
            DateTimeStart = DateTime.Now.AddHours(-1),
            NumberOfDays = 1,
            Comments = "Reservation comments",
            CustomerId = 1,
            BikesIds = new[] { 1, 2, 3 }
        };

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
           .WithMessage("Invalid Reservation");

        await _reservationRepository.DidNotReceive().CreateAsync(Arg.Any<Reservation>());

        result.ShouldHaveValidationErrorFor(request => request.DateTimeStart)
            .WithErrorMessage("Start time must be after current time.");
    }
}
