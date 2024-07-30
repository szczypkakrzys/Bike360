using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.DivingSchoolCustomers;

[Authorize(Policy = Policies.DivingSchool)]
public partial class Index
{
    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public IDivingSchoolCustomerService CustomerService { get; set; }

    public List<CustomerVM> Customers { get; private set; }
    public string Message { get; set; } = string.Empty;
    public string SearchText = "";
    List<CustomerVM> FilteredCustomers => Customers.Where(
            customer => (customer.FirstName + " " + customer.LastName + customer.EmailAddress).Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)).ToList();
    protected override async Task OnInitializedAsync()
    {
        await LoadCustomers();
    }

    protected void CreateCustomer()
    {
        NavManager.NavigateTo("/divingschool/customers/create/");
    }

    protected void CustomerDetails(int id)
    {
        NavManager.NavigateTo($"/divingschool/customers/details/{id}");
    }

    protected async Task LoadCustomers()
    {
        Customers = await CustomerService.GetAllCustomers();
    }

    protected void CustomerCourses(int id)
    {
        NavManager.NavigateTo($"/divingschool/customers/{id}/courses");
    }
}