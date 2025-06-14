using Docmino.Application.Models.External;

namespace Docmino.Application.Abstractions.Email;
public interface IEmailService
{
    Task<string> GetEmailContentAsync(string templateFileName, object data);
    Task SendEmailAsync(EmailRequest mailRequest);
}
