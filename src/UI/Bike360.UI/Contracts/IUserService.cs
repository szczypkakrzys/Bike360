using Bike360.UI.Models;
using Bike360.UI.Models.Emloyee;
using Bike360.UI.Services.Base;

namespace Bike360.UI.Contracts;

public interface IUserService
{
    Task<Response<Guid>> Register(RegisterVM request);
    Task<IEnumerable<EmployeeVM>> GetByRole(Role role);
    Task<Response<Guid>> Delete(string userId);
    Task<Response<Guid>> ChangeEmail(string userId, string newEmail);
}
