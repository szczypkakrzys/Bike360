using Bike360.UI.Contracts;
using Bike360.UI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Bike360.UI.Pages;

public partial class Home
{
    [Inject]
    private AuthenticationStateProvider AuthStateProvider { get; set; }

    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public IAuthenticationService AuthService { get; set; }

    protected async override Task OnInitializedAsync()
    {
        await ((ApiAuthenticationStateProvider)AuthStateProvider).GetAuthenticationStateAsync();
    }

    protected void GoToLogin()
    {
        NavManager.NavigateTo("login/");
    }

    protected void GoToRegister()
    {
        NavManager.NavigateTo("register/");
    }

    protected async void Logout()
    {
        await AuthService.Logout();
    }
}