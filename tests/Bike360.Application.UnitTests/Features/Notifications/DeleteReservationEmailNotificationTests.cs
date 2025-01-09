using Bike360.Application.Contracts.Email;
using Bike360.Application.Features.Notficiations;
using Bike360.Application.Features.Reservations.Events;
using Bike360.Domain;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Bike360.Application.UnitTests.Features.Notifications;

public class DeleteReservationEmailNotificationTests
{
    private readonly IEmailService _emailService;
    private readonly ILogger<ReservationDeletedNotificationHandler> _logger;
    private readonly ReservationDeletedNotificationHandler _handler;

    public DeleteReservationEmailNotificationTests()
    {
        _emailService = Substitute.For<IEmailService>();
        _logger = Substitute.For<ILogger<ReservationDeletedNotificationHandler>>();
        _handler = new ReservationDeletedNotificationHandler(_emailService, _logger);
    }

    [Fact]
    public async Task Handle_ReservationIsDeleted_SendEmailToCustomer()
    {
        // Arrange
        var timeStart = DateTime.UtcNow.AddDays(1);
        var customer = new Customer();
        var reservationDeletedEvent = new ReservationDeletedEvent(
            1,
            customer,
            timeStart,
            timeStart.AddDays(1),
            "Deleted");
        var expectedEmailMessage = $"Your reservation with ID: {reservationDeletedEvent.Id} has been deleted.";

        // Act
        await _handler.Handle(reservationDeletedEvent, CancellationToken.None);

        // Assert
        await _emailService.Received(1).Send(expectedEmailMessage);
    }

    [Fact]
    public async Task Handle_SendingEmailFailed_LogsError()
    {
        var timeStart = DateTime.UtcNow.AddDays(1);
        var customer = new Customer();
        var reservationDeletedEvent = new ReservationDeletedEvent(
            1,
            customer,
            timeStart,
            timeStart.AddDays(1),
            "Deleted");

        _emailService.Send(Arg.Any<string>()).ThrowsAsync(new Exception());

        // Act
        await _handler.Handle(reservationDeletedEvent, CancellationToken.None);

        // Assert
        _logger.Received(1);
    }
}
