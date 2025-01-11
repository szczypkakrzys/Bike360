using Bike360.Application.Contracts.Email;
using Bike360.Application.Features.Reservations.Events;
using Bike360.Application.Features.Reservations.Notficiations.Email;
using Bike360.Domain;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Bike360.Application.UnitTests.Features.Notifications;

public class CreateReservationEmailNotificationTests
{
    private readonly ReservationCreatedNotificationHandler _handler;
    private readonly IEmailService _emailService;
    private readonly ILogger<ReservationCreatedNotificationHandler> _logger;

    public CreateReservationEmailNotificationTests()
    {
        _emailService = Substitute.For<IEmailService>();
        _logger = Substitute.For<ILogger<ReservationCreatedNotificationHandler>>();
        _handler = new ReservationCreatedNotificationHandler(_emailService,
            _logger);
    }

    [Fact]
    public async Task Handle_ReservationIsCreated_SendConfirmationEmailToCustomer()
    {
        // Arrange
        var timeStart = DateTime.UtcNow.AddDays(1);
        var customerData = new Customer();
        var bikes = new List<Bike>();
        var reservationCreatedEvent = new ReservationCreatedEvent(
            1,
            timeStart,
            timeStart.AddDays(1),
            200,
            "Status",
            customerData,
            bikes);

        var expectedEmailMessage = $"Reservation ID: {reservationCreatedEvent.Id} has been created for you.";

        // Act
        await _handler.Handle(reservationCreatedEvent, CancellationToken.None);

        // Assert
        await _emailService.Received(1).Send(expectedEmailMessage);
    }

    [Fact]
    public async Task Handle_SendingEmailFailed_LogsError()
    {
        // Arrange
        var timeStart = DateTime.UtcNow.AddDays(1);
        var customerData = new Customer { EmailAddress = "customer@mail.com" };
        var bikes = new List<Bike>();
        var reservationCreatedEvent = new ReservationCreatedEvent(
            1,
            timeStart,
            timeStart.AddDays(1),
            200,
            "Status",
            customerData,
            bikes);

        _emailService.Send(Arg.Any<string>()).ThrowsAsync(new Exception());

        // Act
        await _handler.Handle(reservationCreatedEvent, CancellationToken.None);

        // Assert
        _logger.Received(1);
    }
}
