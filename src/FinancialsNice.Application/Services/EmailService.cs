using Microsoft.AspNetCore.Hosting;
using FinancialsNice.Application.Dtos.Emails;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Domain.Design_Pattern;
using FinancialsNice.Domain.Entities;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace FinancialsNice.Application.Services;

public class EmailService : IEmailSenderService
{
    private readonly Email _email;
    private readonly IWebHostEnvironment _environment;
    
    private readonly string _emailServer =  Environment.GetEnvironmentVariable("EMAIL_SERVER")!;
    private readonly int _emailPort =  int.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT")!);
    private readonly string _emailSender =  Environment.GetEnvironmentVariable("EMAIL_SENDER")!;
    private readonly string _emailSenderName =  Environment.GetEnvironmentVariable("EMAIL_SENDER_NAME")!;
    private readonly string _emailUsername =  Environment.GetEnvironmentVariable("EMAIL_USERNAME")!;
    private readonly string _emailPassword =  Environment.GetEnvironmentVariable("EMAIL_PASSWORD")!;

    public EmailService(IOptions<Email> smtpSettings, IWebHostEnvironment environment)
    {
        _email = smtpSettings.Value;
        _environment = environment;
    }

    public async Task<ResponseModel<string>> SendEmailAsync(EmailSenderRequest senderRequest)
    {
        var response = new ResponseModel<string>();
        try
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailSenderName, _emailSender));
            mimeMessage.To.Add(new MailboxAddress(senderRequest.Email, senderRequest.Email));
            mimeMessage.Subject = senderRequest.Subject;
            mimeMessage.Body = new TextPart("html") { Text = senderRequest.Body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailServer, _emailPort, true);
                await client.AuthenticateAsync(_emailUsername, _emailPassword);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
            return response.Ok("Ok", "Email sent!");
        }
        catch (Exception)
        {
            return response.Fail("Error", "An Error occured during email send");
        }
    }
    
    public async Task<ResponseModel<string>> SendEmailWithReplyAsync(EmailSenderRequest senderRequest)
    {
        var response = new ResponseModel<string>();
        try
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailSenderName, _emailSender));
            mimeMessage.To.Add(new MailboxAddress(_emailSenderName, _emailSender));
            
            mimeMessage.ReplyTo.Add(new MailboxAddress(senderRequest.Email, senderRequest.Email));
            
            mimeMessage.Subject = senderRequest.Subject;
            mimeMessage.Body = new TextPart("html") { Text = senderRequest.Body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailServer, _emailPort, true);
                await client.AuthenticateAsync(_emailUsername, _emailPassword);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
            return response.Ok("Ok", "Email sent!");
        }
        catch (Exception)
        {
            return response.Fail("Error", "An Error occured during email send");
        }
    }
}