using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Mappers
{
    public class CardMapper
    {
        public static Card ToDomain(CardRequest cardRequest)
        {
            return new Card
            {
                Name = cardRequest.Name,
                Number = cardRequest.Number,
                Company = cardRequest.Company,
                Flag = cardRequest.Flag,
                ExpiredAt = cardRequest.ExpiredAt,
                CardType = cardRequest.CardType,
                Colors = StringConverterHelper.ToString(cardRequest.Colors),
            };
        }

        public static CardResponse ToResponse(Card card)
        {
            return new CardResponse
            {
                Id = card.Id,
                Name = card.Name,
                Number = card.Number,
                Company = card.Company,
                Flag = card.Flag,
                Colors = StringConverterHelper.ToArray(card.Colors),
                ExpiredAt = card.ExpiredAt,
                CardType = card.CardType,
                Status = card.Status,
            };
        }

        public static Card ToUpdate(Card card, CardUpdate update)
        {
            if (update.Name != null && update.Name != card.Name)
                card.Name = update.Name;
            if (update.Number != null && update.Number != card.Number)
                card.Number = update.Number;
            if (update.Company != null && update.Company != card.Company)
                card.Company = update.Company;
            if (update.Flag != null && update.Flag != card.Flag)
                card.Flag = update.Flag;
            if (update.Colors != null && StringConverterHelper.ToString(update.Colors) != card.Colors)
                card.Colors = StringConverterHelper.ToString(update.Colors);
            if (update.ExpiredAt != null && update.ExpiredAt != card.ExpiredAt)
                card.ExpiredAt = update.ExpiredAt;
            if (update.CardType != null && update.CardType != card.CardType)
                card.CardType = (CardType)update.CardType;

            return card;
        }

        public static Card ToUpdateFromRequest(Guid userId, Card card, CardRequest request)
        {
            card.OwnerId = userId;
            card.Name = request.Name;
            card.Number = request.Number;
            card.Company = request.Company;
            card.Flag = request.Flag;
            card.Colors = StringConverterHelper.ToString(request.Colors);
            card.ExpiredAt = request.ExpiredAt;
            card.CardType = request.CardType;

            return card;
        }
    }
}