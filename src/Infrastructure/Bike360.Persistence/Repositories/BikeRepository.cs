using Bike360.Application.Contracts.Persistence;
using Bike360.Domain;
using Bike360.Persistence.DatabaseContext;

namespace Bike360.Persistence.Repositories;

public class BikeRepository : GenericRepository<Bike>, IBikeRepository
{
    public BikeRepository(Bike360DatabaseContext context) : base(context)
    {
    }
}
