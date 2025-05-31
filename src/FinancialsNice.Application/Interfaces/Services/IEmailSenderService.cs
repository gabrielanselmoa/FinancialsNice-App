using FinancialsNice.Application.Dtos.Emails;
using FinancialsNice.Application.Dtos.ResultPattern;

namespace FinancialsNice.Application.Interfaces.Services;

public interface IEmailSenderService
{
    Task<ResponseModel<string>> SendEmailAsync(EmailSenderRequest senderRequest);
}