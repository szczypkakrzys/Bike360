using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Reservations.Events;

public record ReservationCreatedEvent(
    int Id,
    DateTime DateTimeStartInUtc,
    DateTime DateTimeEndInUtc,
    double Cost,
    string Status,
    Customer Customer,
    ICollection<Bike> Bikes,
    string Comments = ""
    ) : INotification;
