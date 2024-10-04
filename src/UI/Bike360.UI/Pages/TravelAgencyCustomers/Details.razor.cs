using AntDesign;
using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.TravelAgency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.TravelAgencyCustomers;

[Authorize(Policy = Policies.TravelAgency)]
public partial class Details
{
    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    ITravelAgencyCustomerService Customer { get; set; }

    [Inject]
    IMessageService _message { get; set; }

    [Parameter]
    public int Id { get; set; }

    TravelAgencyCustomerDetailsVM customer = new();
    public string Message { get; set; } = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        customer = await Customer.GetCustomerDetails(Id);
    }
    protected void EditCustomer(int id)
    {
        NavManager.NavigateTo($"/travelagency/customers/edit/{id}");
    }

    protected async void DeleteCustomer(int id)
    {
        var response = await Customer.DeleteCustomer(id);
        if (response.IsSuccess)
        {
            NavManager.NavigateTo("/travelagency/customers/");
            _message.Info("Poprawnie usuniêto dane klienta");
        }
        else
        {
            _message.Error("Nie uda³o sie usun¹æ danych klienta");
            Message = response.Message;
        }
    }
}