using FinancialsNice.Application.Dtos.PayerReceivers;
using FinancialsNice.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.PayerReceiver;

public class PayerReceiverRequestExample : IExamplesProvider<PayerReceiverRequest>
{
    public PayerReceiverRequest GetExamples()
    {
        return new PayerReceiverRequest(
            Name: "Nome do Cliente/Fornecedor",
            Description: "Cliente frequente para serviços",
            ImgUrl: "https://example.com/payerreceiver_image.jpg",
            UserType: UserType.PERSON
        );
    }
}