using Bike360.Application.Contracts.Email;
using Bike360.Application.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Bike360.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly EmailSettings _emailSettings;

    public EmailService(
        ILogger<EmailService> logger,
        IOptions<EmailSettings> emailSettings)
    {
        _logger = logger;
        _emailSettings = emailSettings.Value;
    }

    public async Task Send(string subject, string content, string receiverName, string receiverEmail)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
        message.To.Add(new MailboxAddress(receiverName, receiverEmail));
        message.Subject = subject;

        message.Body = new TextPart("html")
        {
            Text = content
        };

        using var client = new SmtpClient();

        try
        {
            client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false);
            client.Authenticate(_emailSettings.SenderEmail, _emailSettings.Password);
            client.Send(message);

            _logger.LogInformation("Email sent successfully to {ReceiverEmail}.", receiverEmail);

            client.Disconnect(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while sending email to {ReceiverEmail}", receiverEmail);
            throw;
        }
        finally
        {
            if (client.IsConnected)
                await client.DisconnectAsync(true);
        }
    }
}
