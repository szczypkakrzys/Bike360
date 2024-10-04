using Bike360.UI.Models.Newsletter;
using Bike360.UI.Services.Base;

namespace Bike360.UI.Contracts;

public interface IEmailService
{
    Task<Response<Guid>> SendEmail(EmailVM email);
}
