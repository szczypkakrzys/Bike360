using Bike360.Application.Contracts.Persistence;
using Bike360.Domain.DivingSchool;
using Bike360.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Bike360.Persistence.Repositories;

public class DivingSchoolCustomerRepository : GenericRepository<DivingSchoolCustomer>, IDivingSchoolCustomerRepository
{
    public DivingSchoolCustomerRepository(CustomerDatabaseContext context) : base(context)
    {
    }

    public async Task<DivingSchoolCustomer> GetByIdWithTours(int id)
    {
        var customer = await _context.Set<DivingSchoolCustomer>()
                                    .Include(p => p.DivingCourses)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(q => q.Id == id);

        return customer;
    }

    public async Task<bool> IsCustomerUnique(
         string firstName,
         string lastName,
         string emailAddress)
    {
        var result = await _context.DivingSchoolCustomers
            .AnyAsync(q =>
                q.FirstName == firstName &&
                q.LastName == lastName &&
                q.EmailAddress == emailAddress);

        return !result;
    }

    public async Task<IEnumerable<DivingSchoolCustomer>> GetCustomersByDateOfBirth(DateOnly date)
    {
        return await _context.DivingSchoolCustomers
           .Where(customer => customer.DateOfBirth == date)
           .ToListAsync();
    }
}