using AutoMapper;
using Blazored.LocalStorage;
using Bike360.UI.Contracts;
using Bike360.UI.Models.Notification;
using Bike360.UI.Models.Shared;
using Bike360.UI.Services.Base;
using NotificationType = Bike360.UI.Services.Base.NotificationType;

namespace Bike360.UI.Services;

public class CustomNotificationService : BaseHttpService, ICustomNotificationService
{
    private readonly IMapper _mapper;
    private readonly ITourService _tourService;
    private readonly ICourseService _courseService;
    private readonly ITravelAgencyCustomerService _travelAgencyCustomerService;
    private readonly IDivingSchoolCustomerService _divingSchoolCustomerService;

    public CustomNotificationService(
        IClient client,
        ILocalStorageService localStorageService,
        IMapper mapper,
        ITourService tourService,
        ICourseService courseService,
        ITravelAgencyCustomerService travelAgencyCustomerService,
        IDivingSchoolCustomerService divingSchoolCustomerService) : base(client, localStorageService)
    {
        _mapper = mapper;
        _tourService = tourService;
        _courseService = courseService;
        _travelAgencyCustomerService = travelAgencyCustomerService;
        _divingSchoolCustomerService = divingSchoolCustomerService;
    }

    public List<NotificationVM> Notifications { get; private set; } = [];

    public async Task<Response<Guid>> Delete(int id)
    {
        try
        {
            await AddBearerToken();
            await _client.NotificationDELETEAsync(id);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<List<NotificationVM>> GetUserNotifications(string userId)
    {
        await AddBearerToken();
        var notifications = await _client.NotificationAllAsync(userId);
        return _mapper.Map<List<NotificationVM>>(notifications);
    }

    public async Task<string> MapNotificationName(NotificationVM notification)
    {
        if (notification.Type == NotificationType._0)
            return await TravelAgencyNotificationName(notification);

        else if (notification.Type == NotificationType._1)
            return await DivingSchoolNotificationName(notification);

        return string.Empty;
    }
    public void ClearNotifications()
    {
        Notifications = [];
    }

    public async Task<List<NotificationCustomerVM>> NotificationsCustomers(NotificationVM notification)
    {
        var resultList = new List<NotificationCustomerVM>();

        if (notification.CustomersIdsList != null && notification.ActivityId.HasValue)
        {

            foreach (var customerId in notification.CustomersIdsList)
            {
                CustomerActivityDetailsVM relationDetails = new();
                CustomerVM customerDetails = new();

                if (notification.Type == NotificationType._0)
                {
                    relationDetails = await _travelAgencyCustomerService.CustomerTourDetails(customerId, (int)notification.ActivityId);
                    customerDetails = await _travelAgencyCustomerService.GetCustomerDetails(customerId);
                }
                else if (notification.Type == NotificationType._1)
                {
                    relationDetails = await _divingSchoolCustomerService.CustomerCourseDetails(customerId, (int)notification.ActivityId);
                    customerDetails = await _divingSchoolCustomerService.GetCustomerDetails(customerId);
                }

                var newNotification = new NotificationCustomerVM
                {
                    CustomerFullName = customerDetails.FirstName + " " + customerDetails.LastName
                };


                if (notification.Message == "Advance Payment")
                {
                    newNotification.AmountLeftToPay = relationDetails.AdvancedPaymentLeftToPay;
                }
                else if (notification.Message == "Entire Payment")
                {
                    newNotification.AmountLeftToPay = relationDetails.EntireCostLeftToPay;
                }

                resultList.Add(newNotification);
            }

            return resultList;
        }

        return [];
    }

    private async Task<List<NotificationCustomerVM>> DivingSchoolNotificationCustomers(NotificationVM notification)
    {
        var resultList = new List<NotificationCustomerVM>();

        foreach (var customerId in notification.CustomersIdsList)
        {
            var relationDetails = await _divingSchoolCustomerService.CustomerCourseDetails(customerId, (int)notification.ActivityId);
            var customerDetails = await _divingSchoolCustomerService.GetCustomerDetails(customerId);

            var newNotification = new NotificationCustomerVM
            {
                CustomerFullName = customerDetails.FirstName + " " + customerDetails.LastName
            };


            if (notification.Message == "Advance Payment")
            {
                newNotification.AmountLeftToPay = relationDetails.AdvancedPaymentLeftToPay;
            }
            else if (notification.Message == "Entire Payment")
            {
                newNotification.AmountLeftToPay = relationDetails.EntireCostLeftToPay;
            }

            resultList.Add(newNotification);
        }


        return resultList;
    }

    private async Task<string> TravelAgencyNotificationName(NotificationVM notification)
    {
        if (notification.ActivityId != null)
        {
            var activityDetails = await _tourService.GetTourDetails((int)notification.ActivityId);

            if ((notification.Message == "Advance Payment" || notification.Message == "Entire Payment") && notification.CustomersIdsList != null)
            {
                var paymentType = GetPaymentType(notification.Message);

                var customersMessage = GetAmountOfNotifiacationCustomersMessage(notification.CustomersIdsList);

                return $"Mija termin wpłaty {paymentType} za wycieczkę '{activityDetails.Name}'. {customersMessage}";
            }


            var dateMessage = GetDateMessage(notification.Message);

            return $"{dateMessage} wycieczka: {activityDetails.Name}";
        }
        else if (notification.Message.Contains("Birthday:"))
        {
            return CustomerBirthdayMessage(notification.Message);
        }

        return string.Empty;
    }

    private async Task<string> DivingSchoolNotificationName(NotificationVM notification)
    {
        if (notification.ActivityId != null)
        {
            var activityDetails = await _courseService.GetCourseDetails((int)notification.ActivityId);

            if ((notification.Message == "Advance Payment" || notification.Message == "Entire Payment") && notification.CustomersIdsList != null)
            {
                var paymentType = GetPaymentType(notification.Message);

                var customersMessage = GetAmountOfNotifiacationCustomersMessage(notification.CustomersIdsList);

                return $"Mija termin wpłaty {paymentType} za kurs '{activityDetails.Name}'. {customersMessage}";
            }


            var dateMessage = GetDateMessage(notification.Message);

            return $"{dateMessage} kurs: {activityDetails.Name}";
        }
        else if (notification.Message.Contains("Birthday:"))
        {
            return CustomerBirthdayMessage(notification.Message);
        }

        return string.Empty;
    }

    private static string GetPaymentType(string message)
    {
        if (message == "Advance Payment")
        {
            return "zaliczki";
        }
        else if (message == "Entire Payment")
        {
            return "całej kwoty";
        }
        return string.Empty;
    }

    private static string GetAmountOfNotifiacationCustomersMessage(IEnumerable<int> customers)
    {
        if (customers.Any())
        {
            return $"Liczba klientów, którzy nie uregulowali płatości: {customers.Count()}";
        }
        else
        {
            return "Płatności wszystkich klientów zostały uregulowane.";
        }
    }

    private static string GetDateMessage(string message)
    {
        if (message == "Time Start")
        {
            return "Rozpoczyna się";
        }
        else if (message == "Time End")
        {
            return "Kończy się";
        }

        return string.Empty;
    }

    private static string CustomerBirthdayMessage(string message)
    {
        var startIndex = message.IndexOf(":") + 1;
        var customerName = message[startIndex..];
        return $"Urodziny obchodzi klient: {customerName}.";
    }
}
