using Bike360.Application.Features.Reservations.Results;
using Bike360.Domain;

namespace Bike360.Application.Features.Reservations.Services;

public interface IReservationService
{
    public Task<AvailabilityResult> CheckBikesAvailability(IEnumerable<int> bikesIds, DateTime timeStart, DateTime timeEnd);
    public double CalculateReservationCost(IEnumerable<Bike> reservationBikesEntities, int numberOfDays);
}
