using Bike360.Domain.DivingSchool;

namespace Bike360.Application.Contracts.Persistence;

public interface ICustomersDivingCoursesRelationsRepository : IGenericRepository<CustomersDivingCoursesRelations>
{
    Task<CustomersDivingCoursesRelations> GetCustomerCourseDetails(int customerId, int courseId);
    Task<bool> IsNotAlreadyAssigned(int customerId, int courseId);
}
