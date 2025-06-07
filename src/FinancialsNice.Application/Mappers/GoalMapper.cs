using System.Globalization;
using FinancialsNice.Application.Dtos.Goals;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Mappers;

public static class GoalMapper
{
    public static GoalResponse ToResponse(Goal goal)
    {
        return new GoalResponse()
        {
            Id =  goal.Id,
            Name = goal.Name,
            Balance = goal.Balance,
            Target =  goal.Target,
            GoalType = goal.GoalType,
            Due =  goal.Due,
            CreatedAt =  goal.CreatedAt,
            ModifiedAt =  goal.ModifiedAt,
            Transferences = goal.Transferences.Select(TransferenceMapper.ToResponse).ToList(),
        };
    }

    public static Goal ToDomain(GoalRequest request)
    {
        return new Goal()
        {
            Name = request.Name,
            Target = request.Target,
            GoalType = request.GoalType,
            Due = request.Due
        };
    }

    public static Goal ToUpdate(GoalUpdate update, Goal goal)
    {
        if (update.Name != null && update.Name != goal.Name)
            goal.Name = update.Name;
        if (update.Target != null && update.Target != goal.Target)
            goal.Target = update.Target.Value;
        if (update.Due != null && update.Due != goal.Due)
            goal.Due = update.Due.Value;
        if (update.GoalType != null && update.GoalType != goal.GoalType)
            goal.GoalType = (GoalType)update.GoalType;
        
        goal.ModifiedAt = DateTime.UtcNow;
        
        return goal;
    }
}