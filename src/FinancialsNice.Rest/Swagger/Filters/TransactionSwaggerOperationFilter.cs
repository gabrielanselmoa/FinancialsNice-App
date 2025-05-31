using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class TransactionSwaggerOperationFilter : IOperationFilter
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
            case "GetAllTransactions":
                operation.Summary = "Lists all transactions with filters and pagination";
                operation.Description =
                    "Provides a list of transactions for the authenticated user with search, pagination, and sorting options.";
                AddResponse("200", "List of transactions returned successfully");
                AddResponse("400", "Invalid request parameters.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            case "GetTransactionById":
                operation.Summary = "Gets a transaction by ID";
                operation.Description = "Returns the details of a specific transaction based on its ID.";
                AddResponse("200", "Transaction details returned successfully");
                AddResponse("400", "Invalid ID format or request failed.");
                AddResponse("404", "Transaction not found for the provided ID.");
                break;

            case "GetLastTransactions":
                operation.Summary = "Gets the last 3 transactions next to due";
                operation.Description =
                    "Retrieves the last 3 transactions with their dates closest to the current date for the authenticated user.";
                AddResponse("200", "Last transactions returned successfully");
                AddResponse("400", "Error fetching transactions.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            case "CreateTransaction":
                operation.Summary = "Creates a new transaction";
                operation.Description =
                    "Registers a new transaction in the system with the provided data for the authenticated user.";
                AddResponse("200", "Transaction created successfully");
                AddResponse("400", "Invalid request data or creation failed.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            case "UpdateTransaction":
                operation.Summary = "Updates a transaction by ID";
                operation.Description =
                    "Updates an existing transaction record based on its ID for the authenticated user.";
                AddResponse("200", "Transaction updated successfully");
                AddResponse("400", "Invalid request data or incorrect ID format.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                AddResponse("404", "Transaction not found for the provided ID.");
                break;

            case "SoftDeleteTransaction":
                operation.Summary = "Soft deletes a transaction by ID";
                operation.Description = "Marks an existing transaction as logically deleted.";
                AddResponse("204", "Transaction marked as deleted (soft delete) successfully");
                AddResponse("400", "Invalid ID format or deletion failed.");
                AddResponse("404", "Transaction not found for the provided ID or could not be soft-deleted.");
                break;

            case "HardDeleteTransaction":
                operation.Summary = "Hard deletes a transaction by ID";
                operation.Description = "Permanently deletes a transaction record.";
                AddResponse("204", "Transaction permanently deleted (hard delete) successfully");
                AddResponse("400", "Invalid ID format or deletion failed.");
                AddResponse("404", "Transaction not found for the provided ID or could not be hard-deleted.");
                break;

            default:
                break;
        }
    }
}