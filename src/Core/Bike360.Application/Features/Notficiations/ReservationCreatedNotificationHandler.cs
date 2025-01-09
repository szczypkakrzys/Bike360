using Bike360.Application.Contracts.Email;
using Bike360.Application.Features.Reservations.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Notficiations;

public class ReservationCreatedNotificationHandler : INotificationHandler<ReservationCreatedEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<ReservationCreatedNotificationHandler> _logger;

    public ReservationCreatedNotificationHandler(
        IEmailService emailService,
        ILogger<ReservationCreatedNotificationHandler> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Handle(
        ReservationCreatedEvent reservation,
        CancellationToken cancellationToken)
    {

        //TODO
        // add proper e-mail structure

        var emailContent = $"Reservation ID: {reservation.Id} has been created for you.";

        try
        {
            await _emailService.Send(emailContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send reservation created e-mail notification to {CustomerEmail} for Reservation ID: {ReservationId}", reservation.Customer.EmailAddress, reservation.Id);
        }
    }
}
