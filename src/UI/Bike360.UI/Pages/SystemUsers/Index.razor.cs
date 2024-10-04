using AntDesign;
using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.Emloyee;
using Bike360.UI.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace Bike360.UI.Pages.SystemUsers;

[Authorize(Roles = Roles.Administrator)]
public partial class Index
{
    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    IUserService UserService { get; set; }

    [Inject]
    IMessageService _message { get; set; }

    public IEnumerable<EmployeeVM> Employees { get; set; }
    public string Message { get; set; } = string.Empty;
    public string SearchText = "";

    List<EmployeeVM> FilteredEmployees => Employees.Where(
          employee => (employee.FirstName + " " + employee.LastName + employee.Email).Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)).ToList();

    protected override async Task OnInitializedAsync()
    {
        NavManager.LocationChanged += HandleLocationChanged;
        await UpdateRole(NavManager.Uri);
    }

    private async void HandleLocationChanged(object sender, LocationChangedEventArgs args)
    {
        await UpdateRole(args.Location);
        StateHasChanged();
    }

    string role;
    string pageName;

    private async Task UpdateRole(string uri)
    {
        var query = new Uri(uri).Query;
        role = query.Split('=')[1];

        if (role.Contains("Administrator"))
        {
            pageName = "Administratorzy";
            Employees = await UserService.GetByRole(Role._0);
        }
        else if (role.Contains("TravelAgency"))
        {
            pageName = "Pracownicy biura podró¿y";
            Employees = await UserService.GetByRole(Role._1);
        }
        else if (role.Contains("DivingSchool"))
        {
            pageName = "Pracownicy szko³y nurkowej";
            Employees = await UserService.GetByRole(Role._2);
        }
    }

    private void CreateUser()
    {

    }

    private bool updateEmailModalVisible = false;
    private string choosenEmployeeId;
    private string newEmail;

    private void UpdateEmailModal(string employeeId)
    {
        newEmail = "";
        choosenEmployeeId = employeeId;
        updateEmailModalVisible = true;
    }

    private async Task UpdateEmail()
    {
        var response = await UserService.ChangeEmail(choosenEmployeeId, newEmail);

        if (response.IsSuccess)
        {
            await UpdateRole(NavManager.Uri);
            _message.Success("Poprawnie zaaktualizowano e-mail u¿ytkownika");
        }
        else
        {
            Message = response.Message;
            _message.Error("Nie uda³o siê zaaktualizowaæ e-mail");
        }
    }


    private bool registerModalVisible = false;
    private RegisterVM EmployeeToCreate = new();
    private string registerModalName;
    private Form<RegisterVM> _form;

    private void RegisterModal()
    {
        EmployeeToCreate = new();

        var query = new Uri(NavManager.Uri).Query;
        role = query.Split('=')[1];

        if (role.Contains("Administrator"))
        {
            registerModalName = "Nowe konto administratora";
            EmployeeToCreate.Role = Role._0;
        }
        else if (role.Contains("TravelAgency"))
        {
            registerModalName = "Nowe konto pracownika biura podró¿y";
            EmployeeToCreate.Role = Role._1;
        }
        else if (role.Contains("DivingSchool"))
        {
            registerModalName = "Nowe konto pracownika szko³y nurkowej";
            EmployeeToCreate.Role = Role._2;
        }

        registerModalVisible = true;
    }

    private async Task Register()
    {
        var response = await UserService.Register(EmployeeToCreate);
        if (response.IsSuccess)
        {
            await UpdateRole(NavManager.Uri);
            _message.Success("Utworzono konto nowego u¿ytkownika");
        }
        else
        {
            Message = response.Message;
            _message.Error("Nie uda³o siê utworzyæ nowego konta");
        }
    }

    private async Task HandleOk(MouseEventArgs e)
    {
        _form.Submit();
    }

    private async Task Delete(string id)
    {
        var response = await UserService.Delete(id);
        if (response.IsSuccess)
        {
            await UpdateRole(NavManager.Uri);
            _message.Info("Poprawnie usuniêto konto u¿ytkownika");
        }
        else
        {
            _message.Error("Nie uda³o sie usun¹æ konta u¿ytkownika");
            Message = response.Message;
        }
    }
}