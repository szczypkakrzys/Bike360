using CustomersManagement.Application.Contracts.Persistence;
using CustomersManagement.Application.Exceptions;
using CustomersManagement.Application.Features.TravelAgencyCustomers.Commands.DeleteTravelAgencyCustomer;
using CustomersManagement.Domain.TravelAgency;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace CustomersManagement.Application.UnitTests.Features.Customer.Commands;

public class DeleteTravelAgencyCustomerTests
{
    private readonly ITravelAgencyCustomerRepository _customerRepository;
    private readonly DeleteTravelAgencyCustomerCommandHandler _handler;

    public DeleteTravelAgencyCustomerTests()
    {
        _customerRepository = Substitute.For<ITravelAgencyCustomerRepository>();
        _handler = new DeleteTravelAgencyCustomerCommandHandler(_customerRepository);
    }

    [Fact]
    public async Task Handle_ValidRequest_DeletesCustomer()
    {
        // Arrange
        var customerToDelete = new TravelAgencyCustomer();
        var request = new DeleteTravelAgencyCustomerCommand
        {
            Id = 1
        };

        _customerRepository.GetByIdAsync(request.Id).Returns(customerToDelete);
        _customerRepository.DeleteAsync(customerToDelete).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task Handle_WithNonexistentCustomerId_ThrowsNotFoundException()
    {
        // Arrange
        _customerRepository.GetByIdAsync(Arg.Any<int>()).Returns(default(TravelAgencyCustomer));
        var customerId = 1;
        var request = new DeleteTravelAgencyCustomerCommand
        {
            Id = customerId
        };

        // Act 
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
           .WithMessage($"{nameof(TravelAgencyCustomer)} ({request.Id}) was not found");
    }
}
