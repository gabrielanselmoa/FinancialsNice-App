using FinancialsNice.Application.Dtos.PayerReceivers;
using FinancialsNice.Application.Dtos.Payments;
using FinancialsNice.Application.Dtos.Transactions;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Rest.Swagger.Examples.Card;

namespace FinancialsNice.Rest.Swagger.Examples.Transaction;

public class TransactionResponseExample
{
    public TransactionResponse GetExamples()
    {
        return new TransactionResponse
        {
            Id = Guid.Parse("f0e9d8c7-b6a5-4321-fedc-ba9876543210"),
            Code = "TDI#627",
            Description = "Resposta de transação processada",
            Email = "cliente.exemplo@email.com",
            Currency = "BRL",
            Amount = 150.75m,
            CreatedAt = DateTime.UtcNow.AddDays(-10),
            ScheduledAt = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
            Category = Category.FOOD,
            TransactionType = TransactionType.PAY,
            Status = TransactionStatus.COMPLETED,
            PayerReceiver = new PayerReceiverResponse
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                Name = "Nome do Pagador/Recebedor",
                Description = "Descrição do Pagador/Recebedor",
                ImgUrl = "https://example.com/payerreceiver.png",
                UserType = UserType.PERSON
            },
            Payments = new List<PaymentResponse>
            {
                new PaymentResponse
                {
                    Id = Guid.Parse("p1a2y3m4-e5n6-7890-abcd-ef1234567890"),
                    PaymentType = PaymentType.CREDIT,
                    Amount = 150.75m,
                    Installments = 3,
                    ValuePerInstallment = 50.25m,

                    Card = new CardResponseExample().GetExamples()
                }
            }
        };
    }
}