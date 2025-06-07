using FinancialsNice.Application.Dtos.Goals;
using FinancialsNice.Application.Dtos.Transferences;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Design_Pattern;
using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class GoalService(
    IGoalRepository goalRepository,
    IUserRepository userRepository,
    ITransferenceRepository transferenceRepository)
    : IGoalService
{
    public async Task<PagedResponseModel<ICollection<GoalResponse>>> GetAllAsync(int page, int perPage, Guid userId,
        string? search)
    {
        var response = new PagedResponseModel<ICollection<GoalResponse>>();
        var goals = await goalRepository.GetAllAsync(userId, search);

        if (!goals.Any())
        {
            response.Data = [];
            response.Meta = new MetaData()
            {
                TotalItems = 0,
                TotalPages = 0,
                Page = page,
                PerPage = perPage
            };
            response.Message = "No goals were found for this user.";
            response.Success = true;
            return response;
        }

        var dto = goals.Select(GoalMapper.ToResponse).ToList();

        var totalItems = goals.Count();
        var totalPages = (int)Math.Ceiling((double)totalItems / perPage);

        response.Data = dto;
        response.Meta = new MetaData()
        {
            Page = page,
            PerPage = perPage,
            TotalItems = totalItems,
            TotalPages = totalPages,
            NextPage = page < totalPages,
            PrevPage = page > 1
        };
        response.Message = "Goals retrieved successfully.";
        response.Success = true;
        return response;
    }

    public async Task<ResponseModel<GoalResponse?>> GetByIdAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<GoalResponse?>();
        var goal = await goalRepository.GetByIdAsync(id);

        if (goal == null || goal.OwnerId != userId)
            return response.Fail(null, "Goal not found!");

        var dto = GoalMapper.ToResponse(goal);
        return response.Ok(dto, "Goal retrieved successfully.");
    }

    public async Task<ResponseModel<GoalResponse?>> CreateAsync(GoalRequest request, Guid userId)
    {
        var response = new ResponseModel<GoalResponse?>();
        var user = await userRepository.GetByIdAsync(userId);

        if (user == null)
            return response.Fail(null, "User not found!");

        var goal = GoalMapper.ToDomain(request);
        goal.OwnerId = userId;
        goal.Balance = user.Wallet!.Balance;
        user.Goals?.Add(goal);

        await goalRepository.CreateAsync(goal);
        var dto = GoalMapper.ToResponse(goal);
        return response.Ok(dto, "Goal created successfully.");
    }

    public async Task<ResponseModel<GoalResponse?>> UpdateAsync(Guid id, Guid userId, GoalUpdate update)
    {
        var response = new ResponseModel<GoalResponse?>();
        var existingGoal = await goalRepository.GetByIdAsync(id);

        if (existingGoal == null || existingGoal.OwnerId != userId)
            return response.Fail(null, "Goal not found!");

        var updatedGoal = GoalMapper.ToUpdate(update, existingGoal);
        List<Transference> transferencesDomain;
        decimal sum;
        if (update.Transferences != null && update.Transferences.Any())
        {
            transferencesDomain = update.Transferences.Select(TransferenceMapper.ToDomain).ToList();
            var currentBalance = existingGoal.Transferences?.Sum(t => t.Amount) ?? 0m;
            var incomingAmount = transferencesDomain.Sum(t => t.Amount);
            if ((currentBalance + incomingAmount) > updatedGoal.Target)
                return response.Fail(null, "The total of the transferences is greater than the target!");
            
            foreach (var t in transferencesDomain)
            {
                t.GoalId = id;
                await transferenceRepository.CreateAsync(t);
                // updatedGoal.Transferences.Add(t);
                updatedGoal.Balance += t.Amount;
            }
        }

        await goalRepository.UpdateAsync(id, updatedGoal);
        var dto = GoalMapper.ToResponse(updatedGoal);
        return response.Ok(dto, "Goal updated successfully.");
    }

    public async Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var goal = await goalRepository.GetByIdAsync(id);

        if (goal == null || goal.OwnerId != userId)
            return response.Fail(false, "Goal not found!");

        goal.Status = Status.INACTIVE;
        await goalRepository.SoftDeleteAsync(goal);
        return response.Ok(true, "Soft-deleted successfully")!;
    }

    public async Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var goal = await goalRepository.GetByIdAsync(id);

        if (goal == null || goal.OwnerId != userId)
            return response.Fail(false, "Goal not found!");

        await goalRepository.HardDeleteAsync(goal);
        return response.Ok(true, "Hard-deleted successfully")!;
    }
}