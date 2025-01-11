using Bike360.Application.Contracts.Email;
using Bike360.Application.Features.Reservations.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Reservations.Notficiations.Email;

public class ReservationDeletedNotificationHandler : INotificationHandler<ReservationDeletedEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<ReservationDeletedNotificationHandler> _logger;

    public ReservationDeletedNotificationHandler(
        IEmailService emailService,
        ILogger<ReservationDeletedNotificationHandler> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Handle(
        ReservationDeletedEvent reservation,
        CancellationToken cancellationToken)
    {
        //TODO
        // add proper e-mail structure

        var emailContent = $"Your reservation with ID: {reservation.Id} has been deleted.";

        try
        {
            await _emailService.Send(emailContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send reservation deleted e-mail notification to {CustomerEmail} for Reservation ID: {ReservationId}", reservation.Customer.EmailAddress, reservation.Id);
        }
    }
}
