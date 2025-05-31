using FinancialsNice.Application.Dtos.PayerReceivers;
using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Mappers;

public static class PayerReceiverMapper
{
    public static PayerReceiverResponse ToResponse (PayerReceiver payerReceiver)
    {
        return new PayerReceiverResponse{
            Id = payerReceiver.Id,
            Name = payerReceiver.Name,
            Description = payerReceiver.Description,
            ImgUrl = payerReceiver.ImageUrl,
            UserType = payerReceiver.Type
        };
    }

    public static PayerReceiver ToDomain (PayerReceiverRequest request)
    {
        return new PayerReceiver
        {
            Name = request.Name,
            Description = request.Description,
            ImageUrl = request.ImgUrl,
            Type = request.UserType
        };
    }
    
    public static PayerReceiver ToUpdate(PayerReceiver payerReceiver, PayerReceiverUpdate update)
    {
        if (update.Name != null && update.Name != payerReceiver.Name)
            payerReceiver.Name = update.Name;

        if (update.Description != null && update.Description != payerReceiver.Description)
            payerReceiver.Description = update.Description;

        if (update.ImgUrl != null && update.ImgUrl != payerReceiver.ImageUrl)
            payerReceiver.ImageUrl = update.ImgUrl;

        if (update.UserType != null && update.UserType != payerReceiver.Type)
            payerReceiver.Type = (UserType)update.UserType;

        payerReceiver.ModifiedAt = DateTime.UtcNow;
        
        return payerReceiver;
    }
}