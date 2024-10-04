using AutoMapper;
using Blazored.LocalStorage;
using Bike360.UI.Contracts;
using Bike360.UI.Models.Shared;
using Bike360.UI.Models.TravelAgency;
using Bike360.UI.Services.Base;

namespace Bike360.UI.Services;

public class TravelAgencyCustomerService : BaseHttpService, ITravelAgencyCustomerService
{
    private readonly IMapper _mapper;

    public TravelAgencyCustomerService(
        IClient client,
        IMapper mapper,
        ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<Response<Guid>> CreateCustomer(TravelAgencyCustomerDetailsVM customer)
    {
        try
        {
            await AddBearerToken();
            var createCustomerCommand = _mapper.Map<CreateTravelAgencyCustomerCommand>(customer);
            await _client.CustomersPOST2Async(createCustomerCommand);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> DeleteCustomer(int id)
    {
        try
        {
            await AddBearerToken();
            await _client.CustomersDELETE2Async(id);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<List<CustomerVM>> GetAllCustomers()
    {
        await AddBearerToken();
        var customers = await _client.CustomersAll2Async();
        return _mapper.Map<List<CustomerVM>>(customers);
    }

    public async Task<TravelAgencyCustomerDetailsVM> GetCustomerDetails(int id)
    {
        await AddBearerToken();
        var customer = await _client.CustomersGET2Async(id);
        return _mapper.Map<TravelAgencyCustomerDetailsVM>(customer);
    }

    public async Task<Response<Guid>> UpdateCustomer(int id, TravelAgencyCustomerDetailsVM customer)
    {
        try
        {
            await AddBearerToken();
            var updateCustomerCommand = _mapper.Map<UpdateTravelAgencyCustomerCommand>(customer);
            await _client.CustomersPUT2Async(id.ToString(), updateCustomerCommand);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<List<CustomerActivityVM>> GetCustomerTours(int id)
    {
        await AddBearerToken();
        var customerTours = await _client.ToursAsync(id);
        return _mapper.Map<List<CustomerActivityVM>>(customerTours);
    }

    public async Task<Response<Guid>> AssignCustomerToTour(int customerId, int tourId)
    {
        try
        {
            await AddBearerToken();
            var command = new AssignCustomerToTourCommand
            {
                CustomerId = customerId,
                TourId = tourId
            };
            await _client.TourPOSTAsync(customerId.ToString(), tourId.ToString(), command);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<CustomerActivityDetailsVM> CustomerTourDetails(int customerId, int tourId)
    {
        await AddBearerToken();
        var customer = await _client.TourGETAsync(customerId, tourId);
        return _mapper.Map<CustomerActivityDetailsVM>(customer);
    }

    public async Task<Response<Guid>> UpdateCustomerPayment(int customerId, int tourId, double paymentAmount)
    {
        try
        {
            await AddBearerToken();
            var paymentCommand = new UpdateCustomerTourPaymentCommand
            {
                CustomerId = customerId,
                TourId = tourId,
                PaymentAmount = paymentAmount
            };
            await _client.Payment2Async(customerId.ToString(), tourId.ToString(), paymentCommand);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> RemoveCustomerFromTour(int customerId, int tourId)
    {
        try
        {
            await AddBearerToken();
            var command = new RemoveCustomerFromTourCommand
            {
                CustomerId = customerId,
                TourId = tourId
            };
            await _client.TourDELETEAsync(customerId.ToString(), tourId.ToString(), command);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
