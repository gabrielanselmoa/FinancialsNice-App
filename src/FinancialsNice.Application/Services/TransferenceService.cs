using FinancialsNice.Application.Dtos.Transactions;
using FinancialsNice.Application.Dtos.Transferences;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Design_Pattern;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class TransferenceService(ITransferenceRepository repository, IGoalRepository goalRepository) : ITransferenceService
{
    public async Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId, Guid goalId)
    {
        var response = new ResponseModel<bool>();
        var goal = await goalRepository.GetByIdAsync(goalId);
        if (goal == null) return response.Fail(false, "Goal not found!");
        if (goal.OwnerId != userId) return response.Fail(false, "Goal does not belong to the user!");

        var transference = await repository.GetByIdAsync(id, goalId);
        if (transference == null)
            return response.Fail(false, "Transference not found!");

        goal.Balance -= transference.Amount;
        
        if (goal.Balance - transference.Amount < 0) 
            goal.Balance = 0;
        
        await repository.HardDeleteAsync(transference);
        return response.Ok(true, "Hard-deleted successfully")!;
    }
}