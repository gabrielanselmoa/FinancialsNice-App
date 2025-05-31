using FinancialsNice.Application.Dtos.PayerReceivers;
using FinancialsNice.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.PayerReceiver;

public class PayerReceiverResponseExample : IExamplesProvider<PayerReceiverResponse>
{
    public PayerReceiverResponse GetExamples()
    {
        return new PayerReceiverResponse
        {
            Id = Guid.Parse("9eabaaa4-72c7-4c14-b217-adec6c1cbba6"),
            Name = "Nome do Cliente/Fornecedor",
            Description = "Cliente frequente para serviços",
            ImgUrl = "https://example.com/payerreceiver_image.jpg",
            UserType = UserType.COMPANY
        };
    }
}