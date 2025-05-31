using FinancialsNice.Application.Dtos.Payments;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Rest.Swagger.Examples.Card;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Payment;

public class PaymentResponseExample : IExamplesProvider<PaymentResponse>
{
    public PaymentResponse GetExamples()
    {
        return new PaymentResponse
        {
            Id = Guid.Parse("d953b3c5-4f51-4448-8922-744d56414c25"),
            Amount = 100.50m,
            Installments = 2,
            ValuePerInstallment = 50.25m,
            PaymentType = PaymentType.CREDIT,
            Card = new CardResponseExample().GetExamples()
        };
    }
}