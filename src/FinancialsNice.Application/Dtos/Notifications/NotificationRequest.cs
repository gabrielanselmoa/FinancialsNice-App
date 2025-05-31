namespace FinancialsNice.Application.Dtos.Notifications;

public record NotificationRequest(
    string Message,
    string Type
    );