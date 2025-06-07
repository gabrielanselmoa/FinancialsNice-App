using FinancialsNice.Application.Dtos.Transactions;
using FinancialsNice.Application.Dtos.Transferences;
using FinancialsNice.Domain.Design_Pattern;

namespace FinancialsNice.Application.Interfaces.Services;

public interface ITransferenceService
{
    Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId, Guid goalId);
}