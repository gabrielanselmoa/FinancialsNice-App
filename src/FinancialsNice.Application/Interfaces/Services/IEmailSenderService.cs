using FinancialsNice.Application.Dtos.Emails;
using FinancialsNice.Domain.Design_Pattern;

namespace FinancialsNice.Application.Interfaces.Services;

public interface IEmailSenderService
{
    Task<ResponseModel<string>> SendEmailAsync(EmailSenderRequest senderRequest);
}