using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class WalletSwaggerOperationFilter : IOperationFilter
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
            case "GetWallet":
                operation.Summary = "Retrieves user's wallet";
                operation.Description = "Provides user's wallet.";
                AddResponse("200", "Wallet returned successfully");
                AddResponse("400", "Invalid request");
                AddResponse("401", "Unauthorized");
                break;

            default:
                break;
        }
    }
}