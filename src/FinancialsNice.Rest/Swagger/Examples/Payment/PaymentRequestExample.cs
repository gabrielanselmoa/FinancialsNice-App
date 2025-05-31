using FinancialsNice.Application.Dtos.Payments;
using FinancialsNice.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Payment;

public class PaymentRequestExample : IExamplesProvider<PaymentRequest>
{
    public PaymentRequest GetExamples()
    {
        return new PaymentRequest(
            PaymentType: PaymentType.CREDIT,
            Amount: 100.50m,
            Installments: 2,
            CardId: Guid.Parse("c9d0e1f2-4555-5678-90ab-cdef12345678")
        );
    }
}