using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Application.Dtos.Payments;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Mappers
{
    public static class PaymentMapper
    {
        public static PaymentResponse ToResponse(Payment payment)
        {
            return new PaymentResponse
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Installments = payment.Installments,
                PaymentType = payment.PaymentType,
                Card = payment.Card == null ? null : new CardResponse()
                    {
                        Id = payment.Card.Id,
                        Name = payment.Card.Name,
                        Number = payment.Card.Number,
                        Company = payment.Card.Company,
                        Flag = payment.Card.Flag,
                        Colors = StringConverterHelper.ToArray(payment.Card.Colors),
                        ExpiredAt = payment.Card.ExpiredAt,
                        CardType = payment.Card.CardType,
                        Status = payment.Card.Status
                    }
            };
        }

        public static Payment ToDomain(PaymentRequest request)
        {
            return new Payment
            {
                PaymentType = request.PaymentType,
                Amount = request.Amount,
                Installments = request.Installments,
                CardId = request.CardId,
            };
        }

        public static Payment ToUpdate (Payment payment, PaymentUpdate update)
        {
            if (update.Amount >= 0 && update.Amount != payment.Amount)
                payment.Amount = (decimal)update.Amount;
            if (update.Installments >= 0 && update.Installments != payment.Installments)
                payment.Installments = (int)update.Installments;
            if (update.PaymentType != null && update.PaymentType != payment.PaymentType)
                payment.PaymentType = (PaymentType)update.PaymentType;
            if (update.CardId.HasValue && update.CardId != payment.CardId)
                payment.CardId = update.CardId.Value;

            payment.ModifiedAt = DateTime.UtcNow;
            
            return payment;
        }
    }
}