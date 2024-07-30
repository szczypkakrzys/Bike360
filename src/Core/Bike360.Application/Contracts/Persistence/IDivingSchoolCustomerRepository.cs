using Bike360.Domain.DivingSchool;

namespace Bike360.Application.Contracts.Persistence;

public interface IDivingSchoolCustomerRepository : IGenericRepository<DivingSchoolCustomer>
{
    Task<DivingSchoolCustomer> GetByIdWithTours(int id);
    Task<bool> IsCustomerUnique(
        string firstName,
        string lastName,
        string emailAddress);
    Task<IEnumerable<DivingSchoolCustomer>> GetCustomersByDateOfBirth(DateOnly date);
}
