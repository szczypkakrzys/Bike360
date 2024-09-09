using Bike360.Domain;

namespace Bike360.Application.Contracts.Persistence;

public interface IReservationRepository : IGenericRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetAllBikeReservationsInGivenPeriod(
        int bikeId,
        DateTime periodStart,
        DateTime periodEnd);
}
