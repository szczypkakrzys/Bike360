using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Features.Reservations.Results;

namespace Bike360.Application.Features.Reservations.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IBikeRepository _bikeRepository;

    public ReservationService(
        IReservationRepository reservationRepository,
        IBikeRepository bikeRepository)
    {
        _reservationRepository = reservationRepository;
        _bikeRepository = bikeRepository;
    }

    public AvailabilityResult CheckBikesAvailability(IEnumerable<int> bikesIds, DateTime timeStart, DateTime dateEnd)
    {
        var notAvailableBikesIds = new List<int>();

        foreach (var bikeId in bikesIds)
        {
            if (!IsBikeAvailableInGivenPeriod(bikeId, timeStart, dateEnd))
                notAvailableBikesIds.Add(bikeId);
        }

        if (notAvailableBikesIds.Count == 0)
            return new AvailabilityResult
            {
                AreAvailable = true,
                ErrorMessage = string.Empty
            };

        return new AvailabilityResult
        {
            AreAvailable = false,
            ErrorMessage = PrepareBikesAvailabilityErrorMessage(notAvailableBikesIds)
        };
    }

    public bool IsBikeAvailableInGivenPeriod(int bikeId, DateTime timeStart, DateTime dateEnd)
    {


        return true;
    }

    public int CalculateReservationCost()
    {
        return 0;
    }

    private static string PrepareBikesAvailabilityErrorMessage(List<int> notAvailableBikesIds)
    {
        if (notAvailableBikesIds.Count == 1)
        {
            return $"Bike with ID = {notAvailableBikesIds.First()} is not available in given period";
        }

        var idsString = string.Join(", ", notAvailableBikesIds);
        var message = $"Bikes with IDs = {idsString} are not available in given period";

        return message;
    }
}
