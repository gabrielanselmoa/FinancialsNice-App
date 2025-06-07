using FinancialsNice.Application.Dtos.Goals;
using FinancialsNice.Application.Dtos.Transferences;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Goal;

public class Goal200ResponseExample : IExamplesProvider<GoalResponse>
{
    public GoalResponse GetExamples()
    {
        return new GoalResponse
        {
            Id = Guid.NewGuid(),
            Name = "Buy a new car",
            Target = 50000,
            Balance = 30000,
            GoalType = GoalType.VEHICLE,
            Due = DateConvertHelper.ParseDateOnly("2025-01-01"),
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            Transferences = new List<TransferenceResponse>()
            {
                new TransferenceResponse
                {
                    Id = Guid.NewGuid(),
                    Amount = 5000,
                    Currency = "BRL",
                    Description = "Buy a new car",
                    SentAt = DateConvertHelper.ParseDateOnly("2025-01-01")
                }
            }
        };
    }
}