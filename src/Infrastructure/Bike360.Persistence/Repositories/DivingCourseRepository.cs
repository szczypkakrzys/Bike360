using Bike360.Application.Contracts.Persistence;
using Bike360.Domain.DivingSchool;
using Bike360.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Bike360.Persistence.Repositories;

public class DivingCourseRepository : GenericRepository<DivingCourse>, IDivingCourseRepository
{
    public DivingCourseRepository(CustomerDatabaseContext context) : base(context)
    {
    }

    public async Task<DivingCourse> GetByIdWithParticipants(int id)
    {
        return await _context.Set<DivingCourse>()
                            .Include(p => p.Participants)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<List<DivingCourse>> GetCoursesWithRelationsByDate(DateOnly date)
    {
        return await _context.DivingCourses
           .Where(course => course.TimeStart == date
                          || course.AdvancePaymentDeadline == date
                          || course.EntireAmountPaymentDeadline == date
                          || course.TimeEnd == date)
           .Include(p => p.DivingCourseRelations)
           .AsNoTracking()
           .ToListAsync();
    }
}
