using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class DashboardSwaggerOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var methodName = context.MethodInfo.Name;

        void AddResponse(string statusCode, string description)
        {
            if (!operation.Responses.ContainsKey(statusCode))
            {
                operation.Responses.Add(statusCode, new OpenApiResponse { Description = description });
            }
        }

        switch (methodName)
        {
            case "GetTotalAmountByMonth":
                operation.Summary = "Lists payments and receives summarized";
                operation.Description =
                    "Provides all payments and receives summarized and organized by month in a dashboard.";
                AddResponse("200", "Summary returned successfully");
                AddResponse("400", "Invalid request parameters or error retrieving data");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            case "TotalEarned":
                operation.Summary = "Gets total earned amount";
                operation.Description =
                    "Retrieves the total amount earned for the authenticated user, optionally filtered by year and statuses.";
                AddResponse("200", "Total earned amount returned successfully");
                AddResponse("400", "Invalid request parameters or error retrieving data.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            case "TotalSpent":
                operation.Summary = "Gets total spent amount";
                operation.Description =
                    "Retrieves the total amount spent for the authenticated user, optionally filtered by year and statuses.";
                AddResponse("200", "Total spent amount returned successfully");
                AddResponse("400", "Invalid request parameters or error retrieving data.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            case "WalletHealth":
                operation.Summary = "Gets wallet health summary";
                operation.Description =
                    "Provides a financial health summary for the authenticated user's wallet, optionally filtered by year and statuses.";
                AddResponse("200", "Wallet health summary returned successfully");
                AddResponse("400", "Invalid request parameters or error retrieving data.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            default:
                break;
        }
    }
}