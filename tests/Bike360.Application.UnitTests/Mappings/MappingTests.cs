using AutoMapper;
using Bike360.Application.Features.Customers.Commands.CreateCustomer;
using Bike360.Application.Features.DivingSchoolCustomers.Commands.CreateDivingSchoolCustomer;
using Bike360.Application.Features.DivingSchoolCustomers.Commands.UpdateDivingSchoolCustomer;
using Bike360.Application.Features.DivingSchoolCustomers.Queries.GetAllDivingSchoolCustomers;
using Bike360.Application.Features.DivingSchoolCustomers.Queries.GetDivingSchoolCustomerDetails;
using Bike360.Application.MappingProfiles;
using Bike360.Domain;
using System.Runtime.CompilerServices;

namespace CustomersManagement.Application.UnitTests.Mappings;

public class MappingTests
{
    private readonly MapperConfiguration _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerProfile>();
        });

        _mapper = _configuration.CreateMapper();
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Theory]
    [InlineData(typeof(Customer), typeof(CustomerDto))]
    [InlineData(typeof(Customer), typeof(CustomerDetailsDto))]
    [InlineData(typeof(CreateCustomerCommand), typeof(Customer))]
    [InlineData(typeof(UpdateCustomerCommand), typeof(Customer))]
    [InlineData(typeof(DateTime), typeof(DateOnly))]
    [InlineData(typeof(CreateAddressDto), typeof(Address))]

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
