using Bike360.Application.Contracts.Persistence;
using Bike360.Domain;
using Bike360.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Bike360.Persistence.Repositories;

public class BikeRepository : GenericRepository<Bike>, IBikeRepository
{
    public BikeRepository(Bike360DatabaseContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Bike>> GetByIdsAsync(IEnumerable<int> bikeIds)
    {
        return await _context.Bikes
            .Where(b => bikeIds.Contains(b.Id))
            .ToListAsync();
    }
}
