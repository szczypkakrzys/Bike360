using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Features.Customers.Queries.GetAllCustomers;
using Bike360.Domain;
using FluentAssertions;
using NSubstitute;


namespace Bike360.Application.UnitTests.Features.Customers.Queries;

public class GetAllCustomersTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly GetAllCustomersQueryHandler _handler;

    public GetAllCustomersTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetAllCustomersQueryHandler(_mapper, _customerRepository);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnsListOfCustomerDto()
    {
        // Arrange
        var request = new GetAllCustomersQuery();
        var customers = new List<Customer>();
        var expected = new List<CustomerDto>()
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
        _mapper.Map<IEnumerable<CustomerDto>>(customers).Returns(expected);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}
