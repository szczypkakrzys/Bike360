using Bike360.Application.Contracts.Persistence;
using Bike360.Domain.DivingSchool;
using Bike360.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Bike360.Persistence.Repositories;

public class CustomersDivingCoursesRelationsRepository : GenericRepository<CustomersDivingCoursesRelations>, ICustomersDivingCoursesRelationsRepository
{
    public CustomersDivingCoursesRelationsRepository(CustomerDatabaseContext context) : base(context)
    {
    }

    public async Task<CustomersDivingCoursesRelations> GetCustomerCourseDetails(int customerId, int courseId)
    {
        return await _context.Set<CustomersDivingCoursesRelations>()
                            .AsNoTracking()
                            .FirstOrDefaultAsync(q =>
                                q.CustomerId == customerId &&
                                q.DivingCourseId == courseId);
    }

    public async Task<bool> IsNotAlreadyAssigned(
        int customerId,
        int courseId)
    {
        var result = await _context.CustomersDivingCoursesRelations
            .AnyAsync(q =>
                q.CustomerId == customerId &&
                q.DivingCourseId == courseId);

        return !result;
    }
}
