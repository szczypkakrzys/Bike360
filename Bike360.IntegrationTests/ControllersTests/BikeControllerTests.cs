using Bike360.Api.Models;
using Bike360.Application.Features.Bikes.Commands.CreateBike;
using Bike360.Application.Features.Bikes.Commands.UpdateBike;
using Bike360.Application.Features.Bikes.Queries.GetAllBikes;
using Bike360.Application.Features.Bikes.Queries.GetBikeDetails;
using Bike360.Application.Features.Bikes.Queries.GetBikeReservedDays;
using Bike360.Domain.Models;
using Bike360.IntegrationTests.Helpers;
using Bike360.IntegrationTests.TestFixtures;
using Bike360.IntegrationTests.Tests;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Bike360.IntegrationTests.ControllersTests;

public class BikeControllerTests : IClassFixture<IntegrationTestsWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private readonly int NotExistingId = 999;

    public BikeControllerTests(IntegrationTestsWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnBikesList()
    {
        // Act
        var response = await _httpClient.GetAsync(ApiRoutes.Bikes);
        var result = await response.Content.ReadFromJsonAsync<List<BikeDto>>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNullOrEmpty();
        result.Count.Should().BeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public async Task GetById_BikeDoesNotExist_ShouldReturnNotFound()
    {
        // Act
        var response = await _httpClient.GetAsync(ApiRoutes.Bikes.ById(NotExistingId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_BikesExists_ShouldReturnBikeDeatails()
    {
        // Arrange
        var bikeData = DataFixture.SampleBikes[0];

        // Act
        var response = await _httpClient.GetAsync(ApiRoutes.Bikes.ById(1));
        var result = await response.Content.ReadFromJsonAsync<BikeDetailsDto>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Should().BeEquivalentTo(bikeData, options
            => options
                .Excluding(bike => bike.Id)
                .ExcludingMissingMembers());
    }

    [Fact]
    public async Task Post_ValidBikeData_ShouldReturnCreated()
    {
        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Bikes, DataFixture.CreateTestBikeData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var bikeId = JsonDocument.Parse(jsonResponse).RootElement.GetProperty("id").GetInt32();
        bikeId.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Post_BikeDataIsEmpty_ShouldReturnBadRequestWithErrorMessage()
    {
        // Arrange
        var createRequest = new CreateBikeCommand();

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Bikes, createRequest);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validationErrors.Should().NotBeNull();
        validationErrors.Errors.Should().ContainKeys(
            nameof(CreateBikeCommand.Brand),
            nameof(CreateBikeCommand.Type),
            nameof(CreateBikeCommand.Model),
            nameof(CreateBikeCommand.Size),
            nameof(CreateBikeCommand.Color),
            nameof(CreateBikeCommand.RentCostPerDay));
    }

    [Fact]
    public async Task Post_BikeRentCostIsNegative_ShouldReturnBadRequestWithErrorMessage()
    {
        // Arrange
        var createRequest = DataFixture.CreateTestBikeData;
        createRequest.RentCostPerDay = -200;

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Bikes, createRequest);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validationErrors.Should().NotBeNull();
        validationErrors.Errors.Should().ContainSingle(
            nameof(CreateBikeCommand.RentCostPerDay), "Rent cost must be greater than 0");
    }

    [Fact]
    public async Task Put_BikeDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var request = new UpdateBikeCommand
        {
            Id = NotExistingId,
            Brand = "Test Brand",
            Type = "Test Type",
            Model = "Test Model",
            Size = "Test Size",
            Color = "Test Color",
            RentCostPerDay = 200,
            FrameNumber = "000000000000000",
            Description = "Test Description"
        };

        // Act
        var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Bikes.ById(request.Id), request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Put_ValidBikeData_ShouldReturnNoContent()
    {
        // Arrange
        var request = new UpdateBikeCommand
        {
            Id = 2,
            Brand = "Updated Brand",
            Type = "Updated Type",
            Model = "Updated Model",
            Size = "Updated Size",
            Color = "Updated Color",
            RentCostPerDay = 250,
            FrameNumber = "11111111111111",
            Description = "Updated Description"
        };

        // Act
        var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Bikes.ById(request.Id), request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var updatedBikeData = await _httpClient.GetFromJsonAsync<BikeDetailsDto>(ApiRoutes.Bikes.ById(request.Id));
        updatedBikeData.Should().BeEquivalentTo(request);
    }

    [Fact]
    public async Task Put_BikeDataIsEmpty_ShouldReturnBadRequestWithErrorMessage()
    {
        // Arrange
        var request = new UpdateBikeCommand { Id = 1 };

        // Act
        var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Bikes.ById(request.Id), request);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validationErrors.Should().NotBeNull();
        validationErrors.Errors.Should().ContainKeys(
            nameof(UpdateBikeCommand.Brand),
            nameof(UpdateBikeCommand.Type),
            nameof(UpdateBikeCommand.Model),
            nameof(UpdateBikeCommand.Size),
            nameof(UpdateBikeCommand.Color),
            nameof(UpdateBikeCommand.RentCostPerDay));
    }

    [Fact]
    public async Task Delete_BikeDoesNotExist_ShouldReturnNotFound()
    {
        // Act
        var response = await _httpClient.DeleteAsync(ApiRoutes.Bikes.ById(NotExistingId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_BikeExists_ShouldDeleteAndReturnNoContent()
    {
        // Arrange
        var bikeCreateResponse = await _httpClient.PostAsJsonAsync(ApiRoutes.Bikes, DataFixture.CreateTestBikeData);

        var jsonResponse = await bikeCreateResponse.Content.ReadAsStringAsync();
        using var jsonDocument = JsonDocument.Parse(jsonResponse);
        var bikeId = jsonDocument.RootElement.GetProperty("id").GetInt32();

        // Act
        var response = await _httpClient.DeleteAsync(ApiRoutes.Bikes.ById(bikeId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await _httpClient.GetAsync(ApiRoutes.Bikes.ById(bikeId));
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetBikeReservedDays_ValidRequest_ShouldReturnDateRangeList()
    {
        // Arrange
        var timeStart = DataFixture.SampleReservations[0].DateTimeStartInUtc;
        var timeEnd = timeStart.AddMonths(1);
        var expectedList = new List<DateRange>();
        var expectedDate = new DateRange(timeStart, DataFixture.SampleReservations[0].DateTimeEndInUtc);
        expectedList.Add(expectedDate);

        var timeFormat = "yyyy-MM-ddTHH:mm:ss";
        var timeStartString = timeStart.ToString(timeFormat);
        var timeEndString = timeEnd.ToString(timeFormat);

        // Act
        var response = await _httpClient.GetAsync(ApiRoutes.Bikes.ById(1) + $"/reserved-days?timeStart={timeStartString}&timeEnd={timeEndString}");
        var result = await response.Content.ReadFromJsonAsync<List<DateRange>>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Should().BeEquivalentTo(expectedList);
    }

    [Fact]
    public async Task GetBikeReservedDays_BikeDoesNotExist_ShouldReturnNotFound()
    {
        // Act
        var response = await _httpClient.GetAsync(ApiRoutes.Bikes.ById(NotExistingId) + "/reserved-days?timeStart=2024-01-01T01:01:01&timeEnd=2024-01-02T01:01:01");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetBikeReservedDays_TimeEndIsBeforeTimeStart_ShouldReturnBadRequestWithErrorMessage()
    {
        // Act
        var response = await _httpClient.GetAsync(ApiRoutes.Bikes.ById(NotExistingId) + "/reserved-days?timeStart=2024-01-02T01:01:01&timeEnd=2024-01-01T01:01:01");
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest); validationErrors.Should().NotBeNull();
        validationErrors.Errors.Should().ContainSingle(
            nameof(GetBikeReservedTimeQuery.TimeStart), "Start time must be before time end.");
    }
}
