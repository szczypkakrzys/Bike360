using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Features.Reservations.Results;
using Bike360.Domain;
using Bike360.Domain.Models;

namespace Bike360.Application.Features.Reservations.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<AvailabilityResult> CheckBikesAvailability(
        IEnumerable<int> bikesIds,
        DateTime timeStart,
        DateTime timeEnd)
    {
        var notAvailableBikesIds = new List<int>();

        foreach (var bikeId in bikesIds)
        {
            if (await BikeIsNotAvailableInGivenPeriod(bikeId, timeStart, timeEnd))
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

    //TODO
    //reuse this method to calculate bikes availability
    // can be refactored by putting repo.GetAllBikeReservationsInGivenPeriod() method to BikeIsNotAvailableInGivenPeriod() to now invoke extra method when it's not needed in this flow
    public async Task<List<DateRange>> GetBlockedDays(
        int bikeId,
        DateTime timeStart,
        DateTime timeEnd)
    {
        var reservations = await _reservationRepository.GetAllBikeReservationsInGivenPeriod(
            bikeId,
            timeStart,
            timeEnd);

        var reservationsDates = new List<DateRange>();

        foreach (var reservation in reservations)
        {
            var range = new DateRange(reservation.DateTimeStart, reservation.DateTimeEnd);
            reservationsDates.Add(range);
        }

        return reservationsDates;
    }

    private async Task<bool> BikeIsNotAvailableInGivenPeriod(
        int bikeId,
        DateTime timeStart,
        DateTime timeEnd)
    {
        var blockedDays = await GetBlockedDays(bikeId, timeStart, timeEnd);

        return blockedDays.Count != 0;
    }

    private static string PrepareBikesAvailabilityErrorMessage(List<int> notAvailableBikesIds)
    {
        if (notAvailableBikesIds.Count == 1)
        {
            return $"Bike with ID = {notAvailableBikesIds.First()} is not available in given period";
        }

        var idsString = string.Join(", ", notAvailableBikesIds);
        var message = $"Bikes with IDs = {{ {idsString} }} are not available in given period";

        return message;
    }

    public double CalculateReservationCost(
        IEnumerable<Bike> reservationBikesEntities,
        int numberOfDays)
    {
        return reservationBikesEntities.Sum(bike => bike.RentCostPerDay) * numberOfDays;
    }
}
