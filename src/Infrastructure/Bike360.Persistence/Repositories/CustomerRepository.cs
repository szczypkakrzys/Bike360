using Bike360.Application.Contracts.Persistence;
using Bike360.Domain;
using Bike360.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Bike360.Persistence.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(Bike360DatabaseContext context) : base(context)
    {
    }

    public async Task<bool> IsEmailUnique(string emailAddress)
    {
        var result = await _context.Customers
            .AnyAsync(q => q.EmailAddress == emailAddress);

        return !result;
    }

    public new async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.Address)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}