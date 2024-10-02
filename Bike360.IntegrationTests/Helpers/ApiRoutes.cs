namespace Bike360.IntegrationTests.Helpers;

public static class ApiRoutes
{
    public const string Bikes = "/api/bikes";
    public const string Customers = "/api/customers";
    public const string Reservations = "/api/reservations";
    public const string ReservationStatus = "/api/reservations/status";
    public static string CustomerReservations(int id) => Customers.ById(id) + "/reservations";
}

public static class RouteExtensions
{
    public static string ById(this string baseRoute, int id)
    {
        return $"{baseRoute}/{id}";
    }
}

