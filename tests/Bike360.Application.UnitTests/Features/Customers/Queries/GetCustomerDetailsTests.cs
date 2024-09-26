using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Customers.Queries.GetCustomerDetails;
using Bike360.Domain;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Customers.Queries;

public class GetCustomerDetailsTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCustomerDetailsQueryHandler> _logger;
    private readonly GetCustomerDetailsQueryHandler _handler;

    public GetCustomerDetailsTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetCustomerDetailsQueryHandler>>();
        _handler = new GetCustomerDetailsQueryHandler(_customerRepository, _mapper, _logger);
    }

    [Fact]
    public async Task Handle_WithExisingId_ReturnsCustomerDetailsDto()
    {
        // Arrange
        var customerId = 1;
        var request = new GetCustomerDetailsQuery(customerId);
        var customerDetails = new Customer { Id = customerId };
        var customerDetailsDto = new CustomerDetailsDto
        {
            Id = customerId,
            FirstName = "CustomerFirstName",
            LastName = "CustomerLastaName",
            EmailAddress = "customer@email.com"
        };

        _customerRepository.GetByIdAsync(customerId).Returns(customerDetails);
        _mapper.Map<CustomerDetailsDto>(customerDetails).Returns(customerDetailsDto);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(customerDetailsDto);
    }

    [Fact]
    public async Task Handle_WithNonexistentCustomerId_ThrowsNotFoundException()
    {
        // Arrange
        var customerId = 999;
        var request = new GetCustomerDetailsQuery(customerId);

        _customerRepository.GetByIdAsync(customerId).Returns(default(Customer));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"{nameof(Customer)} with ID = {customerId} was not found");
    }
}
