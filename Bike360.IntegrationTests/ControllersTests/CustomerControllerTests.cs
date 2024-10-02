using Bike360.Api.Models;
using Bike360.Application.Features.Customers.Commands.CreateCustomer;
using Bike360.Application.Features.Customers.Commands.UpdateCustomer;
using Bike360.Application.Features.Customers.Queries.GetAllCustomers;
using Bike360.Application.Features.Customers.Queries.GetCustomerDetails;
using Bike360.Application.Features.Customers.Queries.GetCustomerReservations;
using Bike360.Application.Features.Customers.Shared;
using Bike360.Domain;
using Bike360.IntegrationTests.Helpers;
using Bike360.IntegrationTests.TestFixtures;
using Bike360.IntegrationTests.Tests;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Bike360.IntegrationTests.ControllersTests;

public class CustomerControllerTests : IClassFixture<IntegrationTestsWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private readonly int NotExistingId = 999;

    public CustomerControllerTests(IntegrationTestsWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnCustomersList()
    {
        // Act
        var response = await _httpClient.GetAsync(ApiRoutes.Customers);
        var result = await response.Content.ReadFromJsonAsync<List<CustomerDto>>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNullOrEmpty();
        result.Count.Should().BeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public async Task GetById_CustomerDoesNotExist_ShouldReturnNotFound()
    {
        // Act
        var response = await _httpClient.GetAsync(ApiRoutes.Customers.ById(NotExistingId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Fact]
    public async Task GetById_CustomerExists_ShouldReturnCustomerDetails()
    {
        // Arrange
        var customerData = DataFixture.SampleCustomers[0];

        // Act
        var response = await _httpClient.GetAsync(ApiRoutes.Customers.ById(1));
        var result = await response.Content.ReadFromJsonAsync<CustomerDetailsDto>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Should().BeEquivalentTo(customerData, options
            => options
                .Excluding(customer => customer.Id)
                .Excluding(customer => customer.Address.Id)
                .Excluding(customer => customer.DateOfBirth)
                .ExcludingMissingMembers());
    }

    [Fact]
    public async Task Post_ValidCustomerData_ShouldReturnCreated()
    {
        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Customers, DataFixture.CreateTestCustomerData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var customerId = JsonDocument.Parse(jsonResponse).RootElement.GetProperty("id").GetInt32();
        customerId.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Post_CustomerDataIsEmpty_ShouldReturnBadRequestWithErrorMessage()
    {
        // Arrange
        var createRequest = new CreateCustomerCommand();

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Customers, createRequest);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validationErrors.Should().NotBeNull();
        validationErrors.Errors.Should().ContainKeys(
            nameof(CreateCustomerCommand.FirstName),
            nameof(CreateCustomerCommand.LastName),
            nameof(CreateCustomerCommand.EmailAddress),
            nameof(CreateCustomerCommand.PhoneNumber),
            nameof(CreateCustomerCommand.DateOfBirth),
            nameof(CreateCustomerCommand.Address));
    }

    [Fact]
    public async Task Post_CustomerWithGivenEmailAlreadyExists_ShouldReturnBadRequestWithErrorMessage()
    {
        // Act
        var customerData = DataFixture.CreateTestCustomerData;
        customerData.EmailAddress = DataFixture.SampleCustomers[0].EmailAddress;

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Customers, customerData);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validationErrors.Should().NotBeNull();
        validationErrors.Errors.Should().ContainSingle(
            nameof(CreateCustomerCommand.EmailAddress), "Customer with given e-mail already exists");
    }

    [Fact]
    public async Task Post_CustomerDataIsLackingAddressDetails_ShouldReturnBadRequestWithErrorMessage()
    {
        // Arrange
        var createRequest = new CreateCustomerCommand
        {
            FirstName = "Test FirstName",
            LastName = "Test LastName",
            EmailAddress = "testemail@address.com",
            PhoneNumber = "1234567890",
            DateOfBirth = new DateTime(1999, 01, 01),
            Address = new CreateAddressDto()
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Customers, createRequest);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validationErrors.Should().NotBeNull();
        validationErrors.Errors.Should().ContainKeys(
            $"{nameof(Address)}.{nameof(CreateAddressDto.Country)}",
            $"{nameof(Address)}.{nameof(CreateAddressDto.Voivodeship)}",
            $"{nameof(Address)}.{nameof(CreateAddressDto.PostalCode)}",
            $"{nameof(Address)}.{nameof(CreateAddressDto.City)}",
            $"{nameof(Address)}.{nameof(CreateAddressDto.Street)}",
            $"{nameof(Address)}.{nameof(CreateAddressDto.HouseNumber)}");
    }

    [Fact]
    public async Task Put_CustomerDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var request = new UpdateCustomerCommand
        {
            Id = NotExistingId,
            FirstName = "Test FirstName",
            LastName = "Test LastName",
            EmailAddress = "testemail@address.com",
            PhoneNumber = "1234567890",
            DateOfBirth = new DateTime(1999, 01, 01),
            Address = new AddressDto
            {
                Country = "Country",
                Voivodeship = "Voivodeship",
                PostalCode = "00-000",
                City = "City",
                Street = "Street",
                HouseNumber = "00/00"
            }
        };

        // Act
        var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Customers.ById(request.Id), request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Put_ValidCustomerData_ShouldReturnNoContent()
    {
        // Arrange
        var request = new UpdateCustomerCommand
        {
            Id = 2,
            FirstName = "Updated FirstName",
            LastName = "Updated LastName",
            EmailAddress = "updated@emailaddress.com",
            PhoneNumber = "1234567890",
            DateOfBirth = new DateTime(1999, 01, 01),
            Address = new AddressDto
            {
                Id = 2,
                Country = "Updated Country",
                Voivodeship = "Updated Voivodeship",
                PostalCode = "11-111",
                City = "Updated City",
                Street = "Updated Street",
                HouseNumber = "11/11"
            }
        };

        // Act
        var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Customers.ById(request.Id), request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var updatedCustomerData = await _httpClient.GetFromJsonAsync<CustomerDetailsDto>(ApiRoutes.Customers.ById(request.Id));
        updatedCustomerData.Should().BeEquivalentTo(request, options
            => options.Excluding(customer => customer.DateOfBirth));
    }

    [Fact]
    public async Task Put_CustomerDataIsEmpty_ShouldReturnBadRequestWithErrorMessage()
    {
        // Arrange
        var request = new UpdateCustomerCommand { Id = 1 };

        // Act
        var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Customers.ById(request.Id), request);
        var validationErrors = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validationErrors.Should().NotBeNull();
        validationErrors.Errors.Should().ContainKeys(
            nameof(UpdateCustomerCommand.FirstName),
            nameof(UpdateCustomerCommand.LastName),
            nameof(UpdateCustomerCommand.EmailAddress),
            nameof(UpdateCustomerCommand.PhoneNumber),
            nameof(UpdateCustomerCommand.DateOfBirth),
            nameof(UpdateCustomerCommand.Address));
    }

    [Fact]
    public async Task Delete_CustomerIdDoesNotExist_ShouldReturnNotFound()
    {
        // Act
        var response = await _httpClient.DeleteAsync(ApiRoutes.Customers.ById(NotExistingId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_CustomerExists_ShouldDeleteAndReturnNoContent()
    {
        // Arrange
        var customerData = DataFixture.CreateTestCustomerData;
        customerData.EmailAddress = "secondEmail@address.com";
        var customerCreateResponse = await _httpClient.PostAsJsonAsync(ApiRoutes.Customers, DataFixture.CreateTestCustomerData);

        var jsonResponse = await customerCreateResponse.Content.ReadAsStringAsync();
        using var jsonDocument = JsonDocument.Parse(jsonResponse);
        var customerId = jsonDocument.RootElement.GetProperty("id").GetInt32();

        // Act
        var response = await _httpClient.DeleteAsync(ApiRoutes.Customers.ById(customerId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await _httpClient.GetAsync(ApiRoutes.Customers.ById(customerId));
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
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
                Id = 1,
                DateTimeStartInUtc = reservationEntity.DateTimeStartInUtc,
                DateTimeEndInUtc = reservationEntity.DateTimeEndInUtc,
                Cost = reservationEntity.Cost,
                Comments = reservationEntity.Comments,
                Status = reservationEntity.Status
            }
        };

        // Act
        var response = await _httpClient.GetAsync(ApiRoutes.CustomerReservations(1));
        var result = await response.Content.ReadFromJsonAsync<List<ReservationDto>>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().BeEquivalentTo(expectedList);
    }

    [Fact]
    public async Task GetCustomerReservation_CustomerDoesNotExist_ShouldReturnReservationsList_ShouldReturnNotFound()
    {
        // Act 
        var response = await _httpClient.GetAsync(ApiRoutes.CustomerReservations(NotExistingId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
