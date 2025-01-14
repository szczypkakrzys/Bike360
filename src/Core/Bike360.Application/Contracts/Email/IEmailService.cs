namespace Bike360.Application.Contracts.Email;

public interface IEmailService
{
    Task Send(string subject, string content, string receiverName, string receiverEmail);
}
