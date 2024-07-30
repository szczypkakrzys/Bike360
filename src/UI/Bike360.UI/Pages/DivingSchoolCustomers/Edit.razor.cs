using AntDesign;
using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.DivingSchool;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.DivingSchoolCustomers;

[Authorize(Policy = Policies.DivingSchool)]
public partial class Edit
{
    [Inject]
    IDivingSchoolCustomerService CustomerService { get; set; }

    [Inject]
    NavigationManager NavManager { get; set; }

    [Inject]
    IMessageService _message { get; set; }

    [Parameter]
    public int Id { get; set; }
    public string Message { get; private set; }

    DivingSchoolCustomerDetailsVM Model = new();

    protected override async Task OnParametersSetAsync()
    {
        Model = await CustomerService.GetCustomerDetails(Id);
    }

    private async Task UpdateCustomer()
    {
        var response = await CustomerService.UpdateCustomer(Id, Model);
        if (response.IsSuccess)
        {
            NavManager.NavigateTo("/divingschool/customers/");
            _message.Success("Poprawnie zaaktualizowano dane klienta");
        }
        else
        {
            Message = response.Message;
            _message.Error("Nie uda³o siê edytowaæ danych klienta");
        }
    }
}