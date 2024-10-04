using Bike360.Domain;

namespace Bike360.Application.Contracts.Persistence;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    public Task<bool> IsEmailUnique(string emailAddress);

    public new Task<Customer?> GetByIdAsync(int id);
}
