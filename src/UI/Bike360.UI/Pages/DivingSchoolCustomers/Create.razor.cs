using AntDesign;
using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.DivingSchool;
using Bike360.UI.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.DivingSchoolCustomers;

[Authorize(Policy = Policies.DivingSchool)]
public partial class Create
{
    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public IDivingSchoolCustomerService CustomerService { get; set; }

    [Inject]
    IMessageService _message { get; set; }

    public string Message { get; set; } = string.Empty;

    public DivingSchoolCustomerDetailsVM Model { get; set; } = new DivingSchoolCustomerDetailsVM
    {
        Address = new AddressVM()
    };

    async Task CreateCustomer()
    {
        var response = await CustomerService.CreateCustomer(Model);
        if (response.IsSuccess)
        {
            NavManager.NavigateTo("/divingschool/customers/");
            _message.Success("Poprawnie dodano nowego klienta");
        }
        else
        {
            Message = response.Message;
            _message.Error("Nie uda³o siê dodaæ klienta");
        }
    }
}