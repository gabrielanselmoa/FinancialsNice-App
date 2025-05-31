using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class CardService(ICardRepository cardRepository, IUserRepository userRepository)
    : ICardService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ResponseModel<ICollection<CardResponse>>> GetAllAsync(Guid userId)
    {
        var response = new ResponseModel<ICollection<CardResponse>>();
        var cards = await cardRepository.GetAllAsync(userId);
        
        if (!cards.Any())
            return response.Fail(null, "No cards were found for this user!");

        var dto = cards.Select(CardMapper.ToResponse).ToList();
        return response.Ok(dto, "Cards found successfully.")!;
    }

    public async Task<ResponseModel<CardResponse?>> GetByIdAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<CardResponse?>();
        var card = await cardRepository.GetByIdAsync(id, userId);

        if (card == null || card.OwnerId != userId)
            return response.Fail(null, "Card not found or does not belong to the user!");

        var dto = CardMapper.ToResponse(card);
        return response.Ok(dto, "Card found successfully.");
    }

    public async Task<ResponseModel<CardResponse>> CreateAsync(CardRequest request, Guid userId)
    {
        var response = new ResponseModel<CardResponse>();
        var existingCard = await cardRepository.GetByNumberAsync(request.Number, userId);
        
        if (existingCard != null)
            return response.Fail(null, "Card already exists for this user!");

        var cardDomain = CardMapper.ToDomain(request);
        cardDomain.OwnerId = userId;

        if (!request.Number.Contains('*'))
            cardDomain.Number = CardMask.MaskCardNumber(request.Number);

        await cardRepository.CreateAsync(cardDomain);
        var dto = CardMapper.ToResponse(cardDomain);
        return response.Ok(dto, "Card created successfully.");
    }

    public async Task<ResponseModel<CardResponse>> UpdateAsync(Guid cardId, Guid userId, CardUpdate request)
    {
        var response = new ResponseModel<CardResponse>();
        var card = await cardRepository.GetByIdAsync(cardId, userId);
        
        if (card == null || card.OwnerId != userId)
            return response.Fail(null, "Card not found or does not belong to the user!");

        var updatedCard = CardMapper.ToUpdate(card, request);
        
        if (!string.IsNullOrEmpty(request.Number) && !request.Number.Contains('*'))
            updatedCard.Number = CardMask.MaskCardNumber(request.Number);

        await cardRepository.UpdateAsync(cardId, updatedCard);
        var dto = CardMapper.ToResponse(updatedCard);
        return response.Ok(dto, "Card updated successfully.");
    }

    public async Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var card = await cardRepository.GetByIdAsync(id, userId);

        if (card == null || card.OwnerId != userId)
            return response.Fail(false, "Card not found or does not belong to the user!");

        card.Status = Status.INACTIVE;
        await cardRepository.SoftDeleteAsync(card);
        return response.Ok(true, "Soft-deleted successfully")!;
    }

    public async Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var card = await cardRepository.GetByIdAsync(id, userId);

        if (card == null || card.OwnerId != userId)
            return response.Fail(false, "Card not found or does not belong to the user!");

        await cardRepository.HardDeleteAsync(card);
        return response.Ok(true, "Hard-deleted successfully")!;;
    }
}