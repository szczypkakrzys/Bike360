using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Reservations.Commands.DeleteReservations;
using Bike360.Domain;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Reservations.Commands;

public class DeleteReservationTests
{
    private readonly IReservationRepository _reservationRepository;
    private readonly DeleteReservationCommandHandler _handler;

    public DeleteReservationTests()
    {
        _reservationRepository = Substitute.For<IReservationRepository>();
        _handler = new DeleteReservationCommandHandler(_reservationRepository);
    }

    [Fact]
    public async Task Handle_ValidRequest_DeletesReservation()
    {
        // Arrange
        var reservationId = 1;
        var request = new DeleteReservationCommand { Id = reservationId };
        var reservation = new Reservation { Id = reservationId };

        _reservationRepository.GetByIdAsync(reservationId).Returns(reservation);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task Handle_WithNonexistentReservationId_ThrowsNotFoundException()
    {
        // Arrange
        var reservationId = 999;
        var request = new DeleteReservationCommand { Id = reservationId };

        _reservationRepository.GetByIdAsync(reservationId).Returns(default(Reservation));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"{nameof(Reservation)} with ID = {reservationId} was not found");
    }
}
