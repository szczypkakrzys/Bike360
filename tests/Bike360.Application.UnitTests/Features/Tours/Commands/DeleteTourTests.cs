using CustomersManagement.Application.Contracts.Persistence;
using CustomersManagement.Application.Exceptions;
using CustomersManagement.Application.Features.Tours.Commands.DeleteTour;
using CustomersManagement.Domain.TravelAgency;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace CustomersManagement.Application.UnitTests.Features.Tours.Commands;

public class DeleteTourTests
{
    private readonly ITourRepository _tourRepository;
    private readonly DeleteTourCommandHandler _handler;

    public DeleteTourTests()
    {
        _tourRepository = Substitute.For<ITourRepository>();
        _handler = new DeleteTourCommandHandler(_tourRepository);
    }

    [Fact]
    public async Task Handle_ValidRequest_DeletesTour()
    {
        // Arrange
        var tourToDelete = new Tour();
        var request = new DeleteTourCommand
        {
            Id = 1
        };

        _tourRepository.GetByIdAsync(request.Id).Returns(tourToDelete);
        _tourRepository.DeleteAsync(tourToDelete).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task Handle_WithNonexistentTourId_ThrowsNotFoundException()
    {
        // Arrange
        _tourRepository.GetByIdAsync(Arg.Any<int>()).Returns(default(Tour));

        var request = new DeleteTourCommand
        {
            Id = 1
        };

        // Act 
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
           .WithMessage($"{nameof(Tour)} ({request.Id}) was not found");
    }
}
