using Bike360.Application.Features.Customers.Commands.CreateCustomer;
using Bike360.Domain;

namespace Bike360.IntegrationTests.Tests;

public static class DataFixture
{
    public static List<Customer> SampleCustomers()
    {
        return
        [
            new()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                EmailAddress = "email@address.com",
                PhoneNumber = "1234567890",
                DateOfBirth = new DateOnly(1999, 01, 01),
                Address = new Address
                {
                    Country = "Country",
                    Voivodeship = "Voivodeship",
                    PostalCode = "00-000",
                    City = "City",
                    Street = "Street",
                    HouseNumber = "00/00"
                }
            },
            new()
            {
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                EmailAddress = "email2@address.com",
                PhoneNumber = "1234567890",
                DateOfBirth = new DateOnly(1999, 01, 01),
                Address = new Address
                {
                    Country = "Country",
                    Voivodeship = "Voivodeship",
                    PostalCode = "00-000",
                    City = "City",
                    Street = "Street",
                    HouseNumber = "00/00"
                }
            }
        ];
    }

    public static readonly CreateCustomerCommand CreateTestCustomerData = new()
    {
        FirstName = "Test FirstName",
        LastName = "Test LastName",
        EmailAddress = "testemail@address.com",
        PhoneNumber = "1234567890",
        DateOfBirth = new DateTime(1999, 01, 01),
        Address = new CreateAddressDto
        {
            Country = "Country",
            Voivodeship = "Voivodeship",
            PostalCode = "00-000",
            City = "City",
            Street = "Street",
            HouseNumber = "00/00"
        }
    };
}
