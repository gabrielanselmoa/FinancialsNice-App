using FinancialsNice.Application.Dtos.Payments;
using FinancialsNice.Application.Dtos.Transactions;
using FinancialsNice.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Transaction;

public class TransactionRequestExample : IExamplesProvider<TransactionRequest>
{
    public TransactionRequest GetExamples()
    {
        return new TransactionRequest(
            Description: "Exemplo de descrição de transação de saída",
            Email: "cliente.exemplo@email.com",
            Currency: "BRL",
            Amount: 150.75m,
            ScheduledAt: DateTime.Now.AddDays(7).ToString("dd/MM/yyyy HH:mm:ss"),
            Category: Category.FOOD,
            TransactionType: TransactionType.PAY,
            Status: TransactionStatus.PENDING,
            PayerReceiverId: Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
            Payments: new List<PaymentRequest>
            {
                new PaymentRequest(
                    PaymentType: PaymentType.CREDIT,
                    Amount: 150.75m,
                    Installments: 3,
                    CardId: Guid.Parse("c9d0e1f2-1234-5678-90ab-cdef12345678")
                )
            }
        );
    }
}