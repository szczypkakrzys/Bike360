using Bike360.Application.Contracts.Email;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Features.Reservations.Events;
using Bike360.Application.Features.Reservations.Notficiations.Email;
using Bike360.Domain;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Bike360.Application.UnitTests.Features.Notifications;

public class DeleteReservationEmailNotificationTests
{
    private readonly IEmailService _emailService;
    private readonly ILogger<ReservationDeletedNotificationHandler> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly ReservationDeletedNotificationHandler _handler;

    public DeleteReservationEmailNotificationTests()
    {
        _emailService = Substitute.For<IEmailService>();
        _logger = Substitute.For<ILogger<ReservationDeletedNotificationHandler>>();
        _customerRepository = Substitute.For<ICustomerRepository>();
        _handler = new ReservationDeletedNotificationHandler(_emailService, _logger, _customerRepository);
    }

    [Fact]
    public async Task Handle_ReservationIsDeleted_SendEmailToCustomer()
    {
        // Arrange
        var timeStart = DateTime.UtcNow.AddDays(1);
        var customer = new Customer
        {
            Id = 1,
            FirstName = "Arthur",
            LastName = "Morgan",
            EmailAddress = "amorgan@wild.west"
        };
        var reservationDeletedEvent = new ReservationDeletedEvent(
            1,
            customer.Id,
            timeStart,
            timeStart.AddDays(1),
            "Deleted");

        var expectedEmailMessage = $"Your reservation with ID: {reservationDeletedEvent.Id} has been deleted.";
        var expectedEmailSubject = $"Reservation {reservationDeletedEvent.Id} delete confirmation";
        var expectedReceiverName = $"{customer.FirstName} {customer.LastName}";

        _customerRepository.GetByIdAsync(reservationDeletedEvent.CustomerId).Returns(customer);

        // Act
        await _handler.Handle(reservationDeletedEvent, CancellationToken.None);

        // Assert
        await _emailService.Received(1).Send(expectedEmailSubject, expectedEmailMessage, expectedReceiverName, customer.EmailAddress);
    }

    [Fact]
    public async Task Handle_SendingEmailFailed_LogsError()
    {
        var timeStart = DateTime.UtcNow.AddDays(1);
        var customer = new Customer { Id = 1 };
        var reservationDeletedEvent = new ReservationDeletedEvent(
            1,
            customer.Id,
            timeStart,
            timeStart.AddDays(1),
            "Deleted");

        _customerRepository.GetByIdAsync(reservationDeletedEvent.CustomerId).Returns(customer);

        _emailService.Send(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).ThrowsAsync(new Exception());

        // Act
        await _handler.Handle(reservationDeletedEvent, CancellationToken.None);

        // Assert
        _logger.Received(1);
    }
}
