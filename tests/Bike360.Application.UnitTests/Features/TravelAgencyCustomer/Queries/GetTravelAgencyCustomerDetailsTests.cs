using AutoMapper;
using CustomersManagement.Application.Contracts.Persistence;
using CustomersManagement.Application.Exceptions;
using CustomersManagement.Application.Features.TravelAgencyCustomers.Queries.GetTravelAgencyCustomerDetails;
using CustomersManagement.Domain.TravelAgency;
using FluentAssertions;
using NSubstitute;

namespace CustomersManagement.Application.UnitTests.Features.Customer.Queries;

public class GetTravelAgencyCustomerDetailsTests
{
    private readonly ITravelAgencyCustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly GetTravelAgencyCustomerDetailsQueryHandler _handler;

    public GetTravelAgencyCustomerDetailsTests()
    {
        _customerRepository = Substitute.For<ITravelAgencyCustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetTravelAgencyCustomerDetailsQueryHandler(_customerRepository, _mapper);
    }

    [Fact]
    public async Task Handle_WithExisingId_ReturnsCustomerDetailsDto()
    {
        // Arrange
        var customerId = 1;
        var request = new GetTravelAgencyCustomerDetailsQuery(customerId);
        var customerDetails = new TravelAgencyCustomer { Id = customerId };
        var customerDetailsDto = new TravelAgencyCustomerDetailsDto
        {
            Id = customerId,
            FirstName = "CustomerFirstName",
            LastName = "CustomerLastaName",
            EmailAddress = "customer@email.com"
        };

        _customerRepository.GetByIdAsync(customerId).Returns(customerDetails);
        _mapper.Map<TravelAgencyCustomerDetailsDto>(customerDetails).Returns(customerDetailsDto);

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
        var request = new GetTravelAgencyCustomerDetailsQuery(customerId);

        _customerRepository.GetByIdAsync(customerId).Returns(default(TravelAgencyCustomer));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"{nameof(TravelAgencyCustomer)} ({customerId}) was not found");
    }
}
