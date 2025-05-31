using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class EmailSenderSwaggerOperationFilter : IOperationFilter
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
            case "Contact":
                operation.Summary = "Sends a email";
                operation.Description = "Sends an email using the provided details via a POST request.";
                AddResponse("200", "Email sent successfully");
                AddResponse("400", "Invalid request data or email sending failed");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            default:
                break;
        }
    }
}