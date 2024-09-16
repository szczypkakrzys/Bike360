using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Bikes.Commands.DeleteBike;
using Bike360.Domain;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Bikes.Commands;

public class DeleteBikeTests
{
    private readonly DeleteBikeCommandHandler _handler;
    private readonly IBikeRepository _bikeRepository;

    public DeleteBikeTests()
    {
        _bikeRepository = Substitute.For<IBikeRepository>();
        _handler = new DeleteBikeCommandHandler(_bikeRepository);
    }

    [Fact]
    public async Task Handle_ValidRequest_DeletesBike()
    {
        // Arrange
        var bikeToDelete = new Bike();
        var request = new DeleteBikeCommand { Id = 1 };

        _bikeRepository.GetByIdAsync(request.Id).Returns(bikeToDelete);
        _bikeRepository.DeleteAsync(bikeToDelete).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task Handle_WithNonexistentBikeId_ThrowsNotFoundException()
    {
        // Arrange
        _bikeRepository.GetByIdAsync(Arg.Any<int>()).Returns(default(Bike));
        var request = new DeleteBikeCommand { Id = 1 };

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"{nameof(Bike)} with ID = {request.Id} was not found");
    }
}
