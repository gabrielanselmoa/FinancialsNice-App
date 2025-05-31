using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Card;

public class CardResponseExample : IExamplesProvider<CardResponse>
{
    public CardResponse GetExamples()
    {
        return new CardResponse
        {
            Id = Guid.Parse("c9d0e1f2-4555-5678-90ab-cdef12345678"),
            Name = "Meu Cartão Principal",
            Number = "2423 •••• •••• 2325",
            Company = "Nome do Banco/Emissor",
            Flag = "Visa",
            Colors = new[] { "#4285F4", "#34A853" },
            ExpiredAt = "12/25",
            CardType = CardType.CREDIT,
            Status = Status.ACTIVE,
        };
    }
}