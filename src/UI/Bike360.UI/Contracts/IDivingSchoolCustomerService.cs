using Bike360.UI.Models.DivingSchool;
using Bike360.UI.Models.Shared;
using Bike360.UI.Services.Base;

namespace Bike360.UI.Contracts
{
    public interface IDivingSchoolCustomerService
    {
        Task<List<CustomerVM>> GetAllCustomers();
        Task<DivingSchoolCustomerDetailsVM> GetCustomerDetails(int id);
        Task<Response<Guid>> CreateCustomer(DivingSchoolCustomerDetailsVM customer);
        Task<Response<Guid>> UpdateCustomer(int id, DivingSchoolCustomerDetailsVM customer);
        Task<Response<Guid>> DeleteCustomer(int id);
        Task<List<CustomerActivityVM>> GetCustomerCourses(int id);
        Task<Response<Guid>> AssignCustomerToCourse(int customerId, int courseId);
        Task<CustomerActivityDetailsVM> CustomerCourseDetails(int customerId, int courseId);
        Task<Response<Guid>> UpdateCustomerPayment(int customerId, int courseId, double paymentAmount);
        Task<Response<Guid>> RemoveCustomerFromCourse(int customerId, int courseId);
    }
}
