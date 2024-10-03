using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Reservations.Commands.UpdateReservationStatus;
using Bike360.Domain;
using FluentAssertions;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Reservations.Commands;

public class UpdateReservationStatusTests
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ILogger<UpdateReservationStatusCommandHandler> _logger;
    private readonly UpdateReservationStatusCommandHandler _handler;
    private readonly UpdateReservationStatusCommandValidator _validator;

    public UpdateReservationStatusTests()
    {
        _reservationRepository = Substitute.For<IReservationRepository>();
        _logger = Substitute.For<ILogger<UpdateReservationStatusCommandHandler>>();
        _handler = new UpdateReservationStatusCommandHandler(_reservationRepository, _logger);
        _validator = new UpdateReservationStatusCommandValidator();
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsUnitValue()
    {
        // Arrange
        var reservationId = 1;
        var request = new UpdateReservationStatusCommand
        {
            Id = reservationId,
            Status = "Pending"
        };

        var reservationEntity = new Reservation();

        _reservationRepository.GetByIdAsync(reservationId).Returns(reservationEntity);
        _reservationRepository.UpdateAsync(reservationEntity).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task Handle_WithNonexistentReservationId_ThrowsNotFoundExceptionAndShouldHaveIdValidationError()
    {
        // Arrange
        var reservationId = 999;
        var request = new UpdateReservationStatusCommand
        {
            Id = reservationId,
            Status = "Pending"
        };

        _reservationRepository.GetByIdAsync(reservationId).Returns(default(Reservation));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"{nameof(Reservation)} with ID = {reservationId} was not found");

        await _reservationRepository.DidNotReceive().UpdateAsync(Arg.Any<Reservation>());
    }

    [Fact]
    public async Task Validate_ReservationDataIsEmpty_ThrowsBadRequestExceptionAndShouldHaveValidationErrors()
    {
        // Arrange
        var request = new UpdateReservationStatusCommand();

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid reservation data");

        await _reservationRepository.DidNotReceive().UpdateAsync(Arg.Any<Reservation>());

        result.ShouldHaveValidationErrorFor(request => request.Id)
            .WithErrorMessage("Id is required");
        result.ShouldHaveValidationErrorFor(request => request.Status)
            .WithErrorMessage("Status is required");
    }

    [Fact]
    public async Task Validate_ReservationStatusIsNotValid_ThrowsBadRequestExceptionAndShouldHaveValidationErrors()
    {
        // Arrange
        var request = new UpdateReservationStatusCommand
        {
            Id = 1,
            Status = "Test Status"
        };

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid reservation data");

        await _reservationRepository.DidNotReceive().UpdateAsync(Arg.Any<Reservation>());

        result.ShouldNotHaveValidationErrorFor(request => request.Id);
        result.ShouldHaveValidationErrorFor(request => request.Status)
            .WithErrorMessage("The status value is not valid. It must be one of the following: Pending, In progress, Returned, Completed, Cancelled, Expired, Overdue.");
    }
}
