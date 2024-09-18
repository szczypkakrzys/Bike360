namespace Bike360.IntegrationTests.Helpers;

public static class ApiRoutes
{
    public const string Bikes = "/api/bikes";
    public const string Customers = "/api/customers";
    public const string Reservations = "/api/reservations";
}

public static class RouteExtensions
{
    public static string ById(this string baseRoute, int id)
    {
        return $"{baseRoute}/{id}";
    }
}

