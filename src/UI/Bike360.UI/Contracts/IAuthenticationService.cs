namespace Bike360.UI.Contracts;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(string email, string password);
    Task Logout();
}
