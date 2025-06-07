using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Domain.Design_Pattern;

namespace FinancialsNice.Application.Interfaces.Services
{
    public interface ICardService
    {
        Task<ResponseModel<ICollection<CardResponse>>> GetAllAsync(Guid userId);
        Task<ResponseModel<CardResponse?>> GetByIdAsync(Guid id, Guid userId);
        Task<ResponseModel<CardResponse>> CreateAsync(CardRequest request, Guid userId);
        Task<ResponseModel<CardResponse>> UpdateAsync(Guid cardId, Guid userId, CardUpdate update);
        Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId);
        Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId);
    }
}