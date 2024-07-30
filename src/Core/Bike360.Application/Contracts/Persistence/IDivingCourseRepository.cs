using Bike360.Domain.DivingSchool;

namespace Bike360.Application.Contracts.Persistence;

public interface IDivingCourseRepository : IGenericRepository<DivingCourse>
{
    Task<DivingCourse> GetByIdWithParticipants(int id);
    Task<List<DivingCourse>> GetCoursesWithRelationsByDate(DateOnly date);
}
