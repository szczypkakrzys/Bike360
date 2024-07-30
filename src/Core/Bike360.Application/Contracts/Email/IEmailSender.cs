namespace Bike360.Application.Contracts.Email;

public interface IEmailSender
{
    Task SendEmailAsync(IEnumerable<string> receiversList, string subject, string body);
}
