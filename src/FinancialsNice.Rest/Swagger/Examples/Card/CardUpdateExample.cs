using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Card;

public class CardUpdateExample : IExamplesProvider<CardUpdate>
{
    public CardUpdate GetExamples()
    {
        return new CardUpdate
        {
            Name = "Nome do Cartão Atualizado",
            Number = "••••••••••••5678",
            Company = "Novo Nome da Empresa",
            Flag = "Mastercard",
            Colors = new[] { "#000000", "#FFFFFF" },
            ExpiredAt = "10/27",
            CardType = CardType.DEBIT,
        };
    }
}