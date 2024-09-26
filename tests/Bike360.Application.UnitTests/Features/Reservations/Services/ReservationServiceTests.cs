using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Features.Reservations.Services;
using Bike360.Domain;
using Bike360.Domain.Models;
using FluentAssertions;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Reservations.Services;

public class ReservationServiceTests
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ReservationService _reservationService;

    public ReservationServiceTests()
    {
        _reservationRepository = Substitute.For<IReservationRepository>();
        _reservationService = new ReservationService(_reservationRepository);
    }

    [Fact]
    public async Task GetBlockedDays_ThereAreNoReservationInGivenPeriod_ShouldReturnEmptyCollection()
    {
        // Arrange
        int bikeId = 1;
        var reservations = new List<Reservation>();
        var periodStart = DateTime.Now;
        var periodEnd = DateTime.Now.AddDays(1);

        _reservationRepository.GetAllBikeReservationsInGivenPeriod(bikeId, periodStart, periodEnd).Returns(reservations);

        // Act
        var result = await _reservationService.GetBlockedDays(bikeId, periodStart, periodStart);

        // Assert
        result.Should().BeEquivalentTo(reservations);
    }

    [Fact]
    public async Task GetBlockedDays_ThereAreTwoReservationInGivenPeriod_ShouldReturnCollectionWithProperDatesRanges()
    {
        // Arrange
        int bikeId = 1;
        var timeNow = DateTime.Now;

        var reservations = new List<Reservation>
        {
            new()
            {
                DateTimeStartInUtc = timeNow,
                DateTimeEndInUtc = timeNow.AddDays(2)
            },
            new()
            {
                DateTimeStartInUtc = timeNow.AddDays(3),
                DateTimeEndInUtc = timeNow.AddDays(4)
            }
        };

        var periodStart = timeNow;
        var periodEnd = timeNow.AddDays(4);

        _reservationRepository.GetAllBikeReservationsInGivenPeriod(bikeId, periodStart, periodEnd).Returns(reservations);

        var expectedResult = new List<DateRange>
        {
            new(reservations[0].DateTimeStartInUtc, reservations[0].DateTimeEndInUtc),
            new(reservations[1].DateTimeStartInUtc, reservations[1].DateTimeEndInUtc)
        };

        // Act
        var result = await _reservationService.GetBlockedDays(bikeId, periodStart, periodEnd);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task CheckBikesAvailability_AllBikesAreAvailableInGivenPeriod_ShouldReturnTrueAvailabilityResultWithNoErros()
    {
        // Arrange
        var bikesIds = new List<int> { 1, 2, 3, 4 };
        var periodStart = DateTime.Now;
        var periodEnd = periodStart.AddDays(4);


        _reservationRepository.GetAllBikeReservationsInGivenPeriod(Arg.Any<int>(), periodStart, periodEnd).Returns([]);

        // Act
        var result = await _reservationService.CheckBikesAvailability(bikesIds, periodStart, periodEnd);

        // Assert
        result.AreAvailable.Should().Be(true);
    }

    [Fact]
    public async Task CheckBikesAvailability_TwoBikesAreNotAvailableInGivenPeriod_ShouldReturnFalseAvailabilityResultWithProperErrorMessage()
    {
        // Arrange
        var bikesIds = new List<int> { 1, 2, 3, 4 };
        var periodStart = DateTime.Now;
        var periodEnd = periodStart.AddDays(4);

        var reservations = new List<Reservation>
        {
            new(),
            new()
        };

        _reservationRepository.GetAllBikeReservationsInGivenPeriod(bikesIds[0], periodStart, periodEnd).Returns(reservations);
        _reservationRepository.GetAllBikeReservationsInGivenPeriod(bikesIds[1], periodStart, periodEnd).Returns(reservations);

        // Act
        var result = await _reservationService.CheckBikesAvailability(bikesIds, periodStart, periodEnd);

        // Assert
        result.AreAvailable.Should().Be(false);
        result.ErrorMessage.Should().Be("Bikes with IDs = { 1, 2 } are not available in given period");
    }

    [Fact]
    public void CalculateReservationCost_NumberOfDaysAndBikesCollectionAreGiven_ShouldCalculateCostProperly()
    {
        // Arrange
        var numberOfReservationDays = 5;

        var bikeEntities = new List<Bike>
        {
            new() { RentCostPerDay = 200 },
            new() { RentCostPerDay = 450 }
        };


        // Act 
        var result = _reservationService.CalculateReservationCost(bikeEntities, numberOfReservationDays);

        // Assert
        result.Should().Be(3250);
    }
}
