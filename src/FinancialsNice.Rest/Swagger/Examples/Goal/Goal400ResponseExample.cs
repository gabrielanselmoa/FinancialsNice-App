using FinancialsNice.Application.Dtos.Goals;
using FinancialsNice.Application.Dtos.Transferences;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Goal;

public class Goal400ResponseExample : IExamplesProvider<string>
{
    public string GetExamples()
    {
        return "API: Failed to execute the operation";
    }
}