using Bike360.Application.Contracts.Email;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Reservations.Events;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Reservations.Notficiations.Email;

public class ReservationDeletedNotificationHandler : INotificationHandler<ReservationDeletedEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<ReservationDeletedNotificationHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public ReservationDeletedNotificationHandler(
        IEmailService emailService,
        ILogger<ReservationDeletedNotificationHandler> logger,
        ICustomerRepository customerRepository)
    {
        _emailService = emailService;
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task Handle(
        ReservationDeletedEvent reservation,
        CancellationToken cancellationToken)
    {
        var emailContent = $"Your reservation with ID: {reservation.Id} has been deleted.";
        var emailSubject = $"Reservation {reservation.Id} delete confirmation";

        var customerDetails = await _customerRepository.GetByIdAsync(reservation.CustomerId)
                    ?? throw new NotFoundException(nameof(Customer), reservation.CustomerId);

        var customerFullName = $"{customerDetails.FirstName} {customerDetails.LastName}";

        try
        {
            await _emailService.Send(emailSubject, emailContent, customerFullName, customerDetails.EmailAddress);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send reservation deleted e-mail notification to {CustomerEmail} for Reservation ID: {ReservationId}", customerDetails.EmailAddress, reservation.Id);
        }
    }
}
