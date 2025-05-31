using FinancialsNice.Application.Dtos.Emails;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("email-sender")]
public class EmailSenderController(IEmailSenderService emailSender, ILogger<EmailSenderController> logger)
    : ControllerBase
{

    [HttpPost]
    [Route("contact")]
    public async Task<IActionResult> Contact(
        [FromBody] EmailSenderRequest senderRequest)
    {
        try
        {
            await emailSender.SendEmailAsync(senderRequest);
            logger.LogInformation("Email sent successfully.");
            return StatusCode(200, "Email sent!");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error sending email.");
            return StatusCode(400, e.Message);
        }
    }
}