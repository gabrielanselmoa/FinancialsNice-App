using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class ReportSwaggerOperationFilter : IOperationFilter
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
            case "Generate":
                operation.Summary = "Generates a financial report";
                operation.Description = "Generates and returns a PDF financial report for the authenticated user.";
                AddResponse("200", "Financial report generated and returned successfully (PDF file)");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                AddResponse("500", "An internal server error occurred while generating the report.");
                break;

            default:
                break;
        }
    }
}