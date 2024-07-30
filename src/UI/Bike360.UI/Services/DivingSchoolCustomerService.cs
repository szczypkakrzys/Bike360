using AutoMapper;
using Blazored.LocalStorage;
using Bike360.UI.Contracts;
using Bike360.UI.Models.DivingSchool;
using Bike360.UI.Models.Shared;
using Bike360.UI.Services.Base;

namespace Bike360.UI.Services;

public class DivingSchoolCustomerService : BaseHttpService, IDivingSchoolCustomerService
{
    private readonly IMapper _mapper;

    public DivingSchoolCustomerService(
        IClient client,
        ILocalStorageService localStorageService,
        IMapper mapper) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<Response<Guid>> AssignCustomerToCourse(int customerId, int courseId)
    {
        try
        {
            await AddBearerToken();
            var command = new AssignCustomerToCourseCommand
            {
                CustomerId = customerId,
                CourseId = courseId
            };
            await _client.CoursePOSTAsync(customerId.ToString(), courseId.ToString(), command);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> CreateCustomer(DivingSchoolCustomerDetailsVM customer)
    {
        try
        {
            await AddBearerToken();
            var createCustomerCommand = _mapper.Map<CreateDivingSchoolCustomerCommand>(customer);
            await _client.CustomersPOSTAsync(createCustomerCommand);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<CustomerActivityDetailsVM> CustomerCourseDetails(int customerId, int courseId)
    {
        await AddBearerToken();
        var customer = await _client.CourseGETAsync(customerId, courseId);
        return _mapper.Map<CustomerActivityDetailsVM>(customer);
    }

    public async Task<Response<Guid>> DeleteCustomer(int id)
    {
        try
        {
            await AddBearerToken();
            await _client.CustomersDELETEAsync(id);
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
        var customers = await _client.CustomersAllAsync();
        return _mapper.Map<List<CustomerVM>>(customers);
    }

    public async Task<List<CustomerActivityVM>> GetCustomerCourses(int id)
    {
        await AddBearerToken();
        var customerCourses = await _client.CoursesAsync(id);
        return _mapper.Map<List<CustomerActivityVM>>(customerCourses);
    }

    public async Task<DivingSchoolCustomerDetailsVM> GetCustomerDetails(int id)
    {
        await AddBearerToken();
        var customer = await _client.CustomersGETAsync(id);
        return _mapper.Map<DivingSchoolCustomerDetailsVM>(customer);
    }

    public async Task<Response<Guid>> RemoveCustomerFromCourse(int customerId, int courseId)
    {
        try
        {
            await AddBearerToken();
            var command = new RemoveCustomerFromCourseCommand
            {
                CustomerId = customerId,
                CourseId = courseId
            };
            await _client.CourseDELETEAsync(customerId.ToString(), courseId.ToString(), command);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateCustomer(int id, DivingSchoolCustomerDetailsVM customer)
    {
        try
        {
            await AddBearerToken();
            var updateCustomerCommand = _mapper.Map<UpdateDivingSchoolCustomerCommand>(customer);
            await _client.CustomersPUTAsync(id.ToString(), updateCustomerCommand);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateCustomerPayment(int customerId, int courseId, double paymentAmount)
    {
        try
        {
            await AddBearerToken();
            var paymentCommand = new UpdateCustomerCoursePaymentCommand
            {
                CustomerId = customerId,
                CourseId = courseId,
                PaymentAmount = paymentAmount
            };
            await _client.PaymentAsync(customerId.ToString(), courseId.ToString(), paymentCommand);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
