using Bike360.Api.Models;
using Bike360.Application.Features.Reservations.Commands.CreateReservation;
using Bike360.IntegrationTests.Helpers;
using Bike360.IntegrationTests.TestFixtures;
using Bike360.IntegrationTests.Tests;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Bike360.IntegrationTests.ControllersTests;

public class ReservationControllerTests : IClassFixture<IntegrationTestsWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public ReservationControllerTests(IntegrationTestsWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Post_ValidReservationData_ShouldReturnOK/*NoContent*/()
    {
        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Reservations, DataFixture.CreateTestReservationData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        //var jsonResponse = await response.Content.ReadAsStringAsync();
        //var reservationId = JsonDocument.Parse(jsonResponse).RootElement.GetProperty("id").GetInt32();
        //reservationId.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Post_ReservationDataIsEmpty_ShouldReturnBadRequestWithErrorMessage()
    {
        // Arrange
        var createRequest = new CreateReservationCommand();

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Reservations, createRequest);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validationErrors.Should().NotBeNull();
        validationErrors.Errors.Should().ContainKeys(
            nameof(CreateReservationCommand.DateTimeStart),
            nameof(CreateReservationCommand.NumberOfDays),
            nameof(CreateReservationCommand.CustomerId),
            nameof(CreateReservationCommand.BikesIds));
    }

    [Fact]
    public async Task Post_ReservationBikesAreAlreadyReservedInGivenPeriod_ShouldReturnBadRequestWithErrorMessage()
    {
        // Arrange
        var createRequest = new CreateReservationCommand
        {
            DateTimeStart = DateTime.UtcNow.AddHours(5),
            NumberOfDays = 5,
            CustomerId = 1,
            BikesIds = new[] { 1, 2 }
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Reservations, createRequest);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validationErrors.Should().NotBeNull();

        var idsString = string.Join(", ", createRequest.BikesIds);
        var message = $"Bikes with IDs = {{ {idsString} }} are not available in given period";

        validationErrors.Title.Should().Be(message);
    }

    [Fact]
    public async Task Post_CustomerDoesNotExist_ShouldReturnNotFoundExceptionWithErrorMessage()
    {
        // Arrange
        var createRequest = new CreateReservationCommand
        {
            DateTimeStart = DateTime.UtcNow.AddYears(1),
            NumberOfDays = 5,
            CustomerId = 999,
            BikesIds = new[] { 1, 2 }
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Reservations, createRequest);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        validationErrors.Should().NotBeNull();
        validationErrors.Title.Should().Be($"Customer with ID = {createRequest.CustomerId} was not found");
    }

    [Fact]
    public async Task Post_ReservationDateStartIsEarlierThanCurrentTimeAndNumberofDaysIsZero_ShouldReturnBadRequestWithErrorMessages()
    {
        // Arrange
        var createRequest = new CreateReservationCommand
        {
            DateTimeStart = DateTime.UtcNow.AddDays(-5),
            NumberOfDays = 0,
            CustomerId = 999,
            BikesIds = new[] { 1, 2 }
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Reservations, createRequest);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validationErrors.Should().NotBeNull();
        validationErrors.Errors.Should().ContainKeys(
           nameof(CreateReservationCommand.DateTimeStart),
           nameof(CreateReservationCommand.NumberOfDays));
    }
}
