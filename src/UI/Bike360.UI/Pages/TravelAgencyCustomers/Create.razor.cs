using AntDesign;
using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.Shared;
using Bike360.UI.Models.TravelAgency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.TravelAgencyCustomers;

[Authorize(Policy = Policies.TravelAgency)]
public partial class Create
{
    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public ITravelAgencyCustomerService Customer { get; set; }

    [Inject]
    IMessageService _message { get; set; }

    public string Message { get; set; } = string.Empty;

    public TravelAgencyCustomerDetailsVM Model { get; set; } = new TravelAgencyCustomerDetailsVM
    {
        Address = new AddressVM()
    };

    async Task CreateCustomer()
    {
        var response = await Customer.CreateCustomer(Model);
        if (response.IsSuccess)
        {
            NavManager.NavigateTo("/travelagency/customers/");
            _message.Success("Poprawnie dodano nowego klienta");
        }
        else
        {
            Message = response.Message;
            _message.Error("Nie uda³o siê dodaæ klienta");
        }
    }
}
