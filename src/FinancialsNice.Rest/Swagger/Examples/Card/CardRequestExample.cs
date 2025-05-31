using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Card;

public class CardRequestExample : IExamplesProvider<CardRequest>
{
    public CardRequest GetExamples()
    {
        return new CardRequest(
            Name: "Meu Cartão Principal",
            Number: "2423 •••• •••• 2325",
            Company: "Nome do Banco/Emissor",
            Flag: "Visa",
            ExpiredAt: "12/25",
            Colors: new[] { "#4285F4", "#34A853" },
            CardType: CardType.CREDIT
        );
    }
}