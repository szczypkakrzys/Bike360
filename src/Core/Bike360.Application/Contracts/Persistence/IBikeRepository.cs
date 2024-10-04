using Bike360.Domain;

namespace Bike360.Application.Contracts.Persistence;

public interface IBikeRepository : IGenericRepository<Bike>
{
    Task<IEnumerable<Bike>> GetByIdsAsync(IEnumerable<int> bikeIds);
}
