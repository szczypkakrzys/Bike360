using Bike360.Application.Features.Bikes.Commands.CreateBike;
using Bike360.Application.Features.Customers.Commands.CreateCustomer;
using Bike360.Application.Features.Reservations.Commands.CreateReservation;

namespace Bike360.IntegrationTests.Tests;

public static class DataFixture
{
    public static readonly List<CreateCustomerCommand> SampleCustomers =
    [
        new()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            EmailAddress = "email@address.com",
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
        },
        new()
        {
            FirstName = "FirstName 2",
            LastName = "LastName 2",
            EmailAddress = "email2@address.com",
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
        }
    ];

    public static readonly List<CreateBikeCommand> SampleBikes =
    [
        new()
        {
            Brand = "Brand 1",
            Type = "Type 1",
            Model = "Model 1",
            Size = "Size 1",
            Color = "Color 1",
            RentCostPerDay = 200,
            FrameNumber = "ABCD0123456789",
            Description = "Description 1"
        },
        new()
        {
            Brand = "Brand 2",
            Type = "Type 2",
            Model = "Model 2",
            Size = "Size 2",
            Color = "Color 2",
            RentCostPerDay = 200,
            FrameNumber = "ABCD0123456789",
            Description = "Description 2"
        }
    ];

    private static readonly DateTime timeStart = DateTime.UtcNow.AddDays(1);

    public static readonly CreateReservationCommand SampleReservation =
        new()
        {
            DateTimeStartInUtc = timeStart,
            NumberOfDays = 5,
            Comments = "Comments",
            CustomerId = 1,
            BikesIds = new List<int> { 1, 2 }
        };


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

    public static readonly CreateBikeCommand CreateTestBikeData = new()
    {
        Brand = "Test Brand",
        Type = "Test Type",
        Model = "Test Model",
        Size = "Test Size",
        Color = "Test Color",
        RentCostPerDay = 200,
        FrameNumber = "000000000000000",
        Description = "Test Description"
    };

    public static readonly CreateReservationCommand CreateTestReservationData = new()
    {
        DateTimeStartInUtc = timeStart.AddYears(10),
        NumberOfDays = 5,
        Comments = "Test comments",
        CustomerId = 2,
        BikesIds = new[] { 1, 2 }
    };
}
