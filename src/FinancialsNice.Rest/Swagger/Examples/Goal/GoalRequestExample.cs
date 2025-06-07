using System.Globalization;
using FinancialsNice.Application.Dtos.Goals;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Goal;

public class GoalRequestExample : IExamplesProvider<GoalRequest>
{
    public GoalRequest GetExamples()
    {
        return new GoalRequest
        (
            Name: "Buy a new car",
            Target: 50000,
            GoalType: GoalType.VEHICLE,
            Due: DateConvertHelper.ParseDateOnly("2025-01-01")
        );
    }
}