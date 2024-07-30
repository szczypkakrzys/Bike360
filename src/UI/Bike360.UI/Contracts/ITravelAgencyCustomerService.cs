using Bike360.UI.Models.Shared;
using Bike360.UI.Models.TravelAgency;
using Bike360.UI.Services.Base;

namespace Bike360.UI.Contracts;

public interface ITravelAgencyCustomerService
{
    Task<List<CustomerVM>> GetAllCustomers();
    Task<TravelAgencyCustomerDetailsVM> GetCustomerDetails(int id);
    Task<Response<Guid>> CreateCustomer(TravelAgencyCustomerDetailsVM customer);
    Task<Response<Guid>> UpdateCustomer(int id, TravelAgencyCustomerDetailsVM customer);
    Task<Response<Guid>> DeleteCustomer(int id);
    Task<List<CustomerActivityVM>> GetCustomerTours(int id);
    Task<Response<Guid>> AssignCustomerToTour(int customerId, int tourId);
    Task<CustomerActivityDetailsVM> CustomerTourDetails(int customerId, int tourId);
    Task<Response<Guid>> UpdateCustomerPayment(int customerId, int tourId, double paymentAmount);
    Task<Response<Guid>> RemoveCustomerFromTour(int customerId, int tourId);
}
