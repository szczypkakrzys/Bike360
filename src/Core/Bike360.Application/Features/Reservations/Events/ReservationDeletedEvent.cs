using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Reservations.Events;

public record ReservationDeletedEvent(
    int Id,
    Customer Customer,
    DateTime DateTimeStartInUtc,
    DateTime DateTimeEndInUtc,
    string Status) : INotification;
