using AutoMapper;
using Bike360.Application.Features.Bikes.Commands.CreateBike;
using Bike360.Application.Features.Bikes.Commands.UpdateBike;
using Bike360.Application.Features.Bikes.Queries.GetAllBikes;
using Bike360.Application.Features.Bikes.Queries.GetBikeDetails;
using Bike360.Application.Features.Customers.Commands.CreateCustomer;
using Bike360.Application.Features.Customers.Commands.UpdateCustomer;
using Bike360.Application.Features.Customers.Queries.GetAllCustomers;
using Bike360.Application.Features.Customers.Queries.GetCustomerDetails;
using Bike360.Application.Features.Reservations.Commands.CreateReservation;
using Bike360.Application.MappingProfiles;
using Bike360.Domain;
using System.Runtime.CompilerServices;

namespace Bike360.Application.UnitTests.Mappings;

public class MappingTests
{
    private readonly MapperConfiguration _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerProfile>();
            cfg.AddProfile<BikeProfile>();
            cfg.AddProfile<ReservationProfile>();
        });

        _mapper = _configuration.CreateMapper();
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Theory]
    [InlineData(typeof(DateTime), typeof(DateOnly))]

    // Customer
    [InlineData(typeof(Customer), typeof(CustomerDto))]
    [InlineData(typeof(Customer), typeof(CustomerDetailsDto))]
    [InlineData(typeof(CreateCustomerCommand), typeof(Customer))]
    [InlineData(typeof(UpdateCustomerCommand), typeof(Customer))]
    [InlineData(typeof(CreateAddressDto), typeof(Address))]

    // Bike
    [InlineData(typeof(CreateBikeCommand), typeof(Bike))]
    [InlineData(typeof(UpdateBikeCommand), typeof(Bike))]
    [InlineData(typeof(Bike), typeof(BikeDto))]
    [InlineData(typeof(Bike), typeof(BikeDetailsDto))]

    // Reservation
    [InlineData(typeof(CreateReservationCommand), typeof(Reservation))]

    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private static object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
