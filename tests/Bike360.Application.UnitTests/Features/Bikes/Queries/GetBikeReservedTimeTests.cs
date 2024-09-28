using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Bikes.Queries.GetBikeReservedDays;
using Bike360.Application.Features.Bikes.Queries.GetBikeReservedTime;
using Bike360.Application.Features.Reservations.Services;
using Bike360.Domain;
using Bike360.Domain.Models;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Bikes.Queries;

public class GetBikeReservedTimeTests
{
    private readonly IBikeRepository _bikeRepository;
    private readonly IReservationService _reservationService;
    private readonly ILogger<GetBikeReservedTimeQueryHandler> _logger;
    private readonly GetBikeReservedTimeQueryHandler _handler;
    private readonly GetBikeReservedTimeQueryValidator _validator;

    public GetBikeReservedTimeTests()
    {
        _bikeRepository = Substitute.For<IBikeRepository>();
        _reservationService = Substitute.For<IReservationService>();
        _logger = Substitute.For<ILogger<GetBikeReservedTimeQueryHandler>>();
        _handler = new GetBikeReservedTimeQueryHandler(_bikeRepository, _reservationService, _logger);
        _validator = new GetBikeReservedTimeQueryValidator();
    }

    [Fact]
    public async Task Handle_WithValidData_ReturnsListOfDateRange()
    {
        // Arrange
        var bikeId = 1;
        var bikeEntity = new Bike { Id = bikeId };

        var timeStart = new DateTime(2024, 01, 01);
        var timeEnd = new DateTime(2024, 01, 31);
        var reservedTime = new List<DateRange>
        {
            new(new DateTime(2024,01,10), new DateTime(2024,01,12)),
            new(new DateTime(2024,01,20), new DateTime(2024,01,28))
        };

        var request = new GetBikeReservedTimeQuery(bikeId, timeStart, timeEnd);

        _bikeRepository.GetByIdAsync(bikeId).Returns(bikeEntity);
        _reservationService.GetBlockedDays(bikeId, timeStart, timeEnd).Returns(reservedTime);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(reservedTime);
    }

    [Fact]
    public async Task Handle_WithNonexistentBikeId_ThrowsNotFoundException()
    {
        // Arrange
        var bikeId = 999;
        var timeStart = new DateTime(2024, 01, 01);
        var timeEnd = new DateTime(2024, 01, 31);

        var request = new GetBikeReservedTimeQuery(bikeId, timeStart, timeEnd);

        _bikeRepository.GetByIdAsync(bikeId).Returns(default(Bike));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"{nameof(Bike)} with ID = {bikeId} was not found");
    }

    [Fact]
    public async Task Validate_TimeEndIsBeforeTimeStart_ThrowsBadRequestException()
    {
        // Arrange
        var bikeId = 1;
        var timeStart = new DateTime(2024, 01, 31);
        var timeEnd = new DateTime(2024, 01, 01);

        var request = new GetBikeReservedTimeQuery(bikeId, timeStart, timeEnd);

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid request");

        await _reservationService.DidNotReceive().GetBlockedDays(Arg.Any<int>(), Arg.Any<DateTime>(), Arg.Any<DateTime>());

        result.ShouldHaveValidationErrorFor(request => request.TimeStart)
            .WithErrorMessage("Start time must be before time end.");
    }
}
