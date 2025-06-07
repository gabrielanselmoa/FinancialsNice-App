using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Application.Dtos.Goals;
using FinancialsNice.Application.Dtos.PayerReceivers;
using FinancialsNice.Application.Dtos.Payments;
using FinancialsNice.Application.Dtos.Transactions;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Mappers
{
    public class TransactionMapper
    {
        public static TransactionResponse ToResponse(Transaction transaction)
        {
            return new TransactionResponse
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Code = transaction.Code,
                Email = transaction.Email,
                Currency = transaction.Currency,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt,
                ScheduledAt = transaction.ScheduledAt,
                PayerReceiver = transaction.PayerReceiver == null
                    ? null
                    : new PayerReceiverResponse
                    {
                        Id = transaction.PayerReceiver.Id,
                        Name = transaction.PayerReceiver.Name,
                        Description = transaction.PayerReceiver.Description,
                        ImgUrl = transaction.PayerReceiver.ImageUrl,
                        UserType = transaction.PayerReceiver.Type
                    },
                Category = transaction.Category,
                TransactionType = transaction.TransactionType,
                Status = transaction.TransactionStatus,
                Payments = transaction.Payments.Select(p => new PaymentResponse
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    Installments = p.Installments,
                    ValuePerInstallment = p.ValuePerInstallment,
                    PaymentType = p.PaymentType,
                    Card = p.Card == null
                        ? null
                        : new CardResponse
                        {
                            Id = p.CardId!.Value,
                            Name = p.Card.Name,
                            Number = p.Card.Number,
                            Company = p.Card.Company,
                            Flag = p.Card.Flag,
                            ExpiredAt = p.Card.ExpiredAt,
                            CardType = p.Card.CardType,
                            Status = p.Card.Status
                        }
                }).ToList()
            };
        }

        public static Transaction ToDomain(TransactionRequest request)
        {
            var transaction = new Transaction
            {
                Code = CodeGenerator.GenerateTransactionCode(),
                Description = request.Description,
                Email = request.Email,
                Currency = request.Currency,
                Amount = request.Amount,
                ScheduledAt = DateConvertHelper.ParseDateOnly(request.ScheduledAt),
                Category = request.Category,
                TransactionType = request.TransactionType,
                TransactionStatus = request.Status,
                Payments = request.Payments.Select(p => new Payment()
                {
                    Amount = p.Amount,
                    Installments = p.Installments,
                    ValuePerInstallment = p.Installments == 0 ? 0 : p.Amount / p.Installments,
                    PaymentType = p.PaymentType,
                    CardId = p.CardId,
                }).ToList()
            };

            return transaction;
        }

        public static TransactionPreview ToPreview(Transaction transaction)
        {
            return new TransactionPreview()
            {
                Id =  transaction.Id,
                Category = transaction.Category,
                Description = transaction.Description,
                Date = transaction.ScheduledAt
            };
        }

        public static Transaction ToUpdate(Transaction transaction, TransactionUpdate update)
        {
            if (update.Description != null && update.Description != transaction.Description)
                transaction.Description = update.Description;

            if (update.Email != null && update.Email != transaction.Email)
                transaction.Email = update.Email;

            if (update.Currency != null && update.Currency != transaction.Currency)
                transaction.Currency = update.Currency;

            if (update.Amount != null && update.Amount >= 0 && update.Amount != transaction.Amount)
                transaction.Amount = (decimal)update.Amount;

            if (update.ScheduledAt != null &&
                DateConvertHelper.ParseDateOnly(update.ScheduledAt) != transaction.ScheduledAt)
                transaction.ScheduledAt = DateConvertHelper.ParseDateOnly(update.ScheduledAt);

            if (update.Category != null && update.Category != transaction.Category)
                transaction.Category = (Category)update.Category;

            if (update.TransactionType != null && update.TransactionType != transaction.TransactionType)
                transaction.TransactionType = (TransactionType)update.TransactionType;
            
            if (update.Status != null && update.Status != transaction.TransactionStatus)
                transaction.TransactionStatus = (TransactionStatus)update.Status;

            transaction.ModifiedAt = DateTime.UtcNow;
            
            return transaction;
        }
    }
}