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

    //TODO
    //or just check if e-mail is unique :)
    public async Task<bool> IsCustomerUnique(
         string firstName,
         string lastName,
         string emailAddress)
    {
        var result = await _context.Customers
            .AnyAsync(q =>
                q.FirstName == firstName &&
                q.LastName == lastName &&
                q.EmailAddress == emailAddress);

        return !result;
    }
}