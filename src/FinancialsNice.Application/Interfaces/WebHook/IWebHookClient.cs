using FinancialsNice.Application.Dtos.Notifications;
using Refit;

namespace FinancialsNice.Application.Interfaces.WebHook;

public interface IWebhookClient
{
    [Post("/v1/notify")]
    Task<ApiResponse<string>> SendTransactionNotificationAsync([Body] NotificationRequest request);
}