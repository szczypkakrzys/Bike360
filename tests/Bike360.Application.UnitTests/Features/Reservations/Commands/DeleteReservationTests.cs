using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Reservations.Commands.DeleteReservations;
using Bike360.Application.Features.Reservations.Events;
using Bike360.Domain;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Reservations.Commands;

public class DeleteReservationTests
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ILogger<DeleteReservationCommandHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly DeleteReservationCommandHandler _handler;

    public DeleteReservationTests()
    {
        _reservationRepository = Substitute.For<IReservationRepository>();
        _logger = Substitute.For<ILogger<DeleteReservationCommandHandler>>();
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
        _handler = new DeleteReservationCommandHandler(_reservationRepository, _logger, _mediator, _mapper);
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

    [Fact]
    public async Task Handle_ValidRequest_PublishCustomerNotification()
    {
        // Arrange
        var reservationId = 1;
        var request = new DeleteReservationCommand { Id = reservationId };
        var reservation = new Reservation { Id = reservationId };

        _reservationRepository.GetByIdAsync(reservationId).Returns(reservation);

        var timeStart = DateTime.UtcNow.AddDays(1);
        var expectedNotification = new ReservationDeletedEvent(
            reservationId,
            1,
            timeStart,
            timeStart.AddDays(1),
            "Deleted");
        _mapper.Map<ReservationDeletedEvent>(reservation).Returns(expectedNotification);

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        await _mediator.ReceivedWithAnyArgs(1).Publish(expectedNotification);
    }
}
