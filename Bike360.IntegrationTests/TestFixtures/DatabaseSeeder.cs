using Bike360.IntegrationTests.Helpers;
using Bike360.IntegrationTests.Tests;
using System.Net.Http.Json;

namespace Bike360.IntegrationTests.TestFixtures;

public class DatabaseSeeder
{
    private readonly HttpClient _httpClient;

    public DatabaseSeeder(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SeedCustomerControllerAsync()
    {
        await _httpClient.PostAsJsonAsync(ApiRoutes.Customers, DataFixture.SampleCustomers[0]);
        await _httpClient.PostAsJsonAsync(ApiRoutes.Customers, DataFixture.SampleCustomers[1]);
        await _httpClient.PostAsJsonAsync(ApiRoutes.Bikes, DataFixture.SampleBikes[0]);
        await _httpClient.PostAsJsonAsync(ApiRoutes.Bikes, DataFixture.SampleBikes[1]);
        await _httpClient.PostAsJsonAsync(ApiRoutes.Reservations, DataFixture.SampleReservation);
    }

    public async Task SeedBikeControllerAsync()
    {
        await _httpClient.PostAsJsonAsync(ApiRoutes.Customers, DataFixture.SampleCustomers[0]);
        await _httpClient.PostAsJsonAsync(ApiRoutes.Bikes, DataFixture.SampleBikes[0]);
        await _httpClient.PostAsJsonAsync(ApiRoutes.Bikes, DataFixture.SampleBikes[1]);
        await _httpClient.PostAsJsonAsync(ApiRoutes.Reservations, DataFixture.SampleReservation);
    }

    public async Task SeedReservationControllerAsync()
    {
        await _httpClient.PostAsJsonAsync(ApiRoutes.Customers, DataFixture.SampleCustomers[0]);
        await _httpClient.PostAsJsonAsync(ApiRoutes.Customers, DataFixture.SampleCustomers[1]);
        await _httpClient.PostAsJsonAsync(ApiRoutes.Bikes, DataFixture.SampleBikes[0]);
        await _httpClient.PostAsJsonAsync(ApiRoutes.Bikes, DataFixture.SampleBikes[1]);
        await _httpClient.PostAsJsonAsync(ApiRoutes.Reservations, DataFixture.SampleReservation);
    }
}
