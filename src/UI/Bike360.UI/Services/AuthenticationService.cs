using Blazored.LocalStorage;
using Bike360.UI.Contracts;
using Bike360.UI.Providers;
using Bike360.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace Bike360.UI.Services;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(
        IClient client,
        ILocalStorageService localStorageService,
        AuthenticationStateProvider authenticationStateProvider) : base(client, localStorageService)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        try
        {
            var authenticationRequest = new AuthRequest()
            {
                EmailAddress = email,
                Password = password
            };

            var authenticationResponse = await _client.LoginAsync(authenticationRequest);

            if (authenticationResponse.Token != string.Empty)
            {
                await _localStorage.SetItemAsync("token", authenticationResponse.Token);

                await ((ApiAuthenticationStateProvider)
                    _authenticationStateProvider).LoggedIn();

                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public async Task Logout()
    {
        await ((ApiAuthenticationStateProvider)
            _authenticationStateProvider).LoggedOut();
    }
}
