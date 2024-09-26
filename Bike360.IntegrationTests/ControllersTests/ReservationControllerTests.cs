using Bike360.Api.Models;
using Bike360.Application.Features.Reservations.Commands.CreateReservation;
using Bike360.Application.Features.Reservations.Commands.UpdateReservationStatus;
using Bike360.Application.Features.Reservations.Constants;
using Bike360.Application.Features.Reservations.Queries.GetCustomerReservations;
using Bike360.Application.Features.Reservations.Queries.GetReservationDetails;
using Bike360.IntegrationTests.Helpers;
using Bike360.IntegrationTests.TestFixtures;
using Bike360.IntegrationTests.Tests;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Bike360.IntegrationTests.ControllersTests;

public class ReservationControllerTests : IClassFixture<IntegrationTestsWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private readonly int NotExistingId = 999;

    public ReservationControllerTests(IntegrationTestsWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Post_ValidReservationData_ShouldReturnCreated()
    {
        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Reservations, DataFixture.CreateTestReservationData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var reservationId = JsonDocument.Parse(jsonResponse).RootElement.GetProperty("id").GetInt32();
        reservationId.Should().BeGreaterThan(0);
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

    [Fact]
    public async Task GetCustomerReservation_CustomerExists_ShouldReturnReservationsList()
    {
        // Arrange
        var reservationEntity = DataFixture.SampleReservations[0];
        var expectedList = new List<ReservationDto>
        {
            new()
            {
                DateTimeStart = reservationEntity.DateTimeStartInUtc,
                DateTimeEnd = reservationEntity.DateTimeEndInUtc,
                Cost = reservationEntity.Cost,
                Comments = reservationEntity.Comments,
                Status = reservationEntity.Status
            }
        };

        // Act
        var response = await _httpClient.GetAsync(ApiRoutes.UserReservations.ById(1));
        var result = await response.Content.ReadFromJsonAsync<List<ReservationDto>>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().BeEquivalentTo(expectedList);
    }

    [Fact]
    public async Task GetCustomerReservation_CustomerDoesNotExist_ShouldReturnReservationsList_ShouldReturnNotFound()
    {
        // Act 
        var response = await _httpClient.GetAsync(ApiRoutes.UserReservations.ById(NotExistingId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_ReservationExists_ShouldReturnReservationDetails()
    {
        // Arrange
        var reservationBike1 = DataFixture.SampleBikes[0];
        var reservationBike2 = DataFixture.SampleBikes[1];
        var reservationCustomer = DataFixture.SampleCustomers[0];
        var reservationData = DataFixture.SampleReservations[0];

        var expectedReservationData = new ReservationDetailsDto
        {
            DateTimeStart = reservationData.DateTimeStartInUtc,
            DateTimeEnd = reservationData.DateTimeEndInUtc,
            Cost = reservationData.Cost,
            Status = reservationData.Status,
            Comments = reservationData.Comments,
            CustomerData = new CustomerDto
            {
                Id = reservationData.CustomerId,
                FirstName = reservationCustomer.FirstName,
                LastName = reservationCustomer.LastName,
                EmailAddress = reservationCustomer.EmailAddress,
                PhoneNumber = reservationCustomer.PhoneNumber
            },
            BikesData = new List<BikeDto>
            {
                new()
                {
                    Id = 1,
                    Brand = reservationBike1.Brand,
                    Model = reservationBike1.Model,
                    Type = reservationBike1.Type,
                    Size = reservationBike1.Size,
                    RentCostPerDay = reservationBike1.RentCostPerDay
                },
                new()
                {
                    Id = 2,
                    Brand = reservationBike2.Brand,
                    Model = reservationBike2.Model,
                    Type = reservationBike2.Type,
                    Size = reservationBike2.Size,
                    RentCostPerDay = reservationBike2.RentCostPerDay
                }
            }
        };

        // Act 
        var response = await _httpClient.GetAsync(ApiRoutes.Reservations.ById(1));
        var result = await response.Content.ReadFromJsonAsync<ReservationDetailsDto>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().BeEquivalentTo(expectedReservationData);
    }

    [Fact]
    public async Task GetById_ReservationDoesNotExist_ShouldReturnNotFound()
    {
        // Act
        var response = await _httpClient.GetAsync(ApiRoutes.Reservations.ById(NotExistingId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ReservationIdDoesNotExist_ShouldReturnNotFound()
    {
        // Act
        var response = await _httpClient.DeleteAsync(ApiRoutes.Reservations.ById(NotExistingId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ReservationExists_ShouldDeleteAndReturnNoContent()
    {
        // Arrange
        var reservationData = DataFixture.CreateTestReservationData;
        reservationData.DateTimeStart = reservationData.DateTimeStart.AddDays(20);

        var reservationCreateResponse = await _httpClient.PostAsJsonAsync(ApiRoutes.Reservations, reservationData);

        var jsonResponse = await reservationCreateResponse.Content.ReadAsStringAsync();
        using var jsonDocument = JsonDocument.Parse(jsonResponse);
        var reservationId = jsonDocument.RootElement.GetProperty("id").GetInt32();

        // Act
        var response = await _httpClient.DeleteAsync(ApiRoutes.Reservations.ById(reservationId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await _httpClient.GetAsync(ApiRoutes.Reservations.ById(reservationId));
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PatchReservationStatus_ValidStatus_ShouldReturnNoContent()
    {
        // Arrange
        var request = new UpdateReservationStatusCommand
        {
            Id = 1,
            Status = ReservationStatus.Pending
        };

        // Act
        var response = await _httpClient.PatchAsJsonAsync(ApiRoutes.Reservations, request);

        // Assert 
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var updatedReservationData = await _httpClient.GetFromJsonAsync<ReservationDetailsDto>(ApiRoutes.Reservations.ById(request.Id));
        updatedReservationData.Status.Should().Be(request.Status);
    }

    [Fact]
    public async Task PatchReservationStatus_ReservationDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var request = new UpdateReservationStatusCommand
        {
            Id = NotExistingId,
            Status = ReservationStatus.Pending
        };

        // Act
        var response = await _httpClient.PatchAsJsonAsync(ApiRoutes.Reservations, request);

        // Assert 
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PatchReservationStatus_UpdateDataIsEmpty_ShouldReturnBadRequestWithErrorMessage()
    {
        // Arrange
        var request = new UpdateReservationStatusCommand();

        // Act
        var response = await _httpClient.PatchAsJsonAsync(ApiRoutes.Reservations, request);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert 
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validationErrors.Should().NotBeNull();
        validationErrors.Errors.Should().ContainKeys(
            nameof(UpdateReservationStatusCommand.Id),
            nameof(UpdateReservationStatusCommand.Status));
    }

    [Fact]
    public async Task PatchReservationStatus_StatusIsNotValid_ShouldReturnBadRequestWithErrorMessage()
    {
        // Arrange
        var request = new UpdateReservationStatusCommand
        {
            Id = 1,
            Status = "Test Status 12345"
        };

        // Act
        var response = await _httpClient.PatchAsJsonAsync(ApiRoutes.Reservations, request);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert 
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validationErrors.Should().NotBeNull();

        validationErrors.Errors.Should().ContainSingle(
           nameof(UpdateReservationStatusCommand.Status), "The status value is not valid. It must be one of the following: Pending.");
    }
}
