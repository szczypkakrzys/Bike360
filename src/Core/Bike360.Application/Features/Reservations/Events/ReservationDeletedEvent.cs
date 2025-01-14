using MediatR;

namespace Bike360.Application.Features.Reservations.Events;

public record ReservationDeletedEvent(
    int Id,
    int CustomerId,
    DateTime DateTimeStartInUtc,
    DateTime DateTimeEndInUtc,
    string Status) : INotification;
