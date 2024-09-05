using Bike360.Application.Features.Reservations.Results;

namespace Bike360.Application.Features.Reservations.Services;

public interface IReservationService
{
    public bool IsBikeAvailableInGivenPeriod(int bikeId, DateTime timeStart, DateTime dateEnd);
    public AvailabilityResult CheckBikesAvailability(IEnumerable<int> bikesIds, DateTime timeStart, DateTime dateEnd);
}
