using Bike360.Domain;

namespace Bike360.Application.Contracts.Persistence;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<bool> IsCustomerUnique(
        string firstName,
        string lastName,
        string emailAddress);
}
