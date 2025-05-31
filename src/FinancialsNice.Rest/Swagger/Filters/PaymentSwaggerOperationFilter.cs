using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class PaymentSwaggerOperationFilter : IOperationFilter
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
            case "GetAllPayments":
                operation.Summary = "Lists all payments with pagination";
                operation.Description = "Provides a paginated list of all payments for the authenticated user.";
                AddResponse("200", "List of payments returned successfully");
                AddResponse("400", "Invalid request parameters");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            case "GetPaymentById":
                operation.Summary = "Gets a payment by ID";
                operation.Description = "Retrieves a specific payment by its ID for the authenticated user.";
                AddResponse("200", "Payment returned successfully");
                AddResponse("400", "Invalid ID format or request failed");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                AddResponse("404", "Payment not found for the provided ID and user.");
                break;

            case "UpdatePayment":
                operation.Summary = "Updates a payment by ID";
                operation.Description =
                    "Updates an existing payment record based on its ID for the authenticated user.";
                AddResponse("200", "Payment updated successfully");
                AddResponse("400", "Invalid request data or incorrect ID format.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                AddResponse("404", "Payment not found for the provided ID and user.");
                break;

            case "SoftDeletePayment":
                operation.Summary = "Soft deletes a payment by ID";
                operation.Description = "Marks an existing payment as logically deleted for the authenticated user.";
                AddResponse("204", "Payment marked as deleted (soft delete) successfully");
                AddResponse("400", "Invalid ID format or deletion failed.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                AddResponse("404", "Payment not found for the provided ID or could not be soft-deleted.");
                break;

            case "HardDeletePayment":
                operation.Summary = "Hard deletes a payment by ID";
                operation.Description =
                    "Permanently deletes a payment record from the database for the authenticated user.";
                AddResponse("204", "Payment permanently deleted (hard delete) successfully");
                AddResponse("400", "Invalid ID format or deletion failed.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                AddResponse("404", "Payment not found for the provided ID or could not be hard-deleted.");
                break;

            default:
                break;
        }
    }
}