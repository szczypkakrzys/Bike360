using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.DivingSchoolCustomers.Commands.DeleteDivingSchoolCustomer;
using Bike360.Domain;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Customers.Commands;

public class DeleteCustomerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly DeleteCustomerCommandHandler _handler;

    public DeleteCustomerTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _handler = new DeleteCustomerCommandHandler(_customerRepository);
    }

    [Fact]
    public async Task Handle_ValidRequest_DeletesCustomer()
    {
        // Arrange
        var customerToDelete = new Customer();
        var request = new DeleteCustomerCommand
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
        _customerRepository.GetByIdAsync(Arg.Any<int>()).Returns(default(Customer));
        var customerId = 1;
        var request = new DeleteCustomerCommand
        {
            Id = customerId
        };

        // Act 
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
           .WithMessage($"{nameof(Customer)} ({request.Id}) was not found");
    }
}
