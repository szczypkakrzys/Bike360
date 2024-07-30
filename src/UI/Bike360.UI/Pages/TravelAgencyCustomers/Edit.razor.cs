using AntDesign;
using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.TravelAgency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.TravelAgencyCustomers;

[Authorize(Policy = Policies.TravelAgency)]
public partial class Edit
{
    [Inject]
    ITravelAgencyCustomerService Customer { get; set; }

    [Inject]
    NavigationManager NavManager { get; set; }

    [Inject]
    IMessageService _message { get; set; }

    [Parameter]
    public int Id { get; set; }
    public string Message { get; private set; }

    TravelAgencyCustomerDetailsVM Model = new();

    protected override async Task OnParametersSetAsync()
    {
        Model = await Customer.GetCustomerDetails(Id);
    }

    private async Task UpdateCustomer()
    {
        var response = await Customer.UpdateCustomer(Id, Model);
        if (response.IsSuccess)
        {
            NavManager.NavigateTo("/travelagency/customers/");
            _message.Success("Poprawnie zaaktualizowano dane klienta");
        }
        else
        {
            Message = response.Message;
            _message.Error("Nie uda³o siê edytowaæ danych klienta");
        }
    }
}