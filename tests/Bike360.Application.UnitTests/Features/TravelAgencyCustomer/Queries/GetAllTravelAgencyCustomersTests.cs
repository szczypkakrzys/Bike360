using AutoMapper;
using CustomersManagement.Application.Contracts.Persistence;
using CustomersManagement.Application.Features.TravelAgencyCustomers.Queries.GetAllTravelAgencyCustomers;
using CustomersManagement.Domain.TravelAgency;
using FluentAssertions;
using NSubstitute;


namespace CustomersManagement.Application.UnitTests.Features.Customer.Queries;

public class GetAllTravelAgencyCustomersTests
{
    private readonly ITravelAgencyCustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly GetTravelAgencyCustomersQueryHandler _handler;

    public GetAllTravelAgencyCustomersTests()
    {
        _customerRepository = Substitute.For<ITravelAgencyCustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetTravelAgencyCustomersQueryHandler(_mapper, _customerRepository);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnsListOfCustomerDto()
    {
        // Arrange
        var request = new GetTravelAgencyCustomersQuery();
        var customers = new List<TravelAgencyCustomer>();
        var expected = new List<TravelAgencyCustomerDto>()
        {
            new()
            {
                Id = 1,
                FirstName = "Customer1FirstName",
                LastName = "Customer1LastName",
            },
            new()
            {
                Id = 2,
                FirstName = "Customer2FirstName",
                LastName = "Customer2LastName",
            }
        };

        _customerRepository.GetAsync().Returns(customers);
        _mapper.Map<List<TravelAgencyCustomerDto>>(customers).Returns(expected);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}
