using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class CardSwaggerOperationFilter : IOperationFilter
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
            case "GetAllCards":
                operation.Summary = "Lists all cards for the authenticated user";
                operation.Description = "Provides a list of all cards associated with the currently authenticated user.";
                AddResponse("200", "List of cards returned successfully");
                AddResponse("400", "Error retrieving cards");
                AddResponse("401", "Unauthorized");
                break;

            case "GetCardById":
                operation.Summary = "Gets a card by ID";
                operation.Description = "Retrieves a specific card based on its ID for the authenticated user.";
                AddResponse("200", "Card returned successfully");
                AddResponse("400", "Invalid ID format");
                AddResponse("401", "Unauthorized");
                AddResponse("404", "Card not found");
                break;

            case "CreateCard":
                operation.Summary = "Creates a new card";
                operation.Description = "Registers a new card in the system for the authenticated user.";
                AddResponse("201", "Card created successfully");
                AddResponse("400", "Invalid request data or creation failed");
                AddResponse("401", "Owner is not authenticated");
                break;

            case "UpdateCard":
                operation.Summary = "Updates a card by ID";
                operation.Description = "Updates an existing card record based on its ID for the authenticated user.";
                AddResponse("200", "Card updated successfully");
                AddResponse("400", "Invalid request data or ID format");
                AddResponse("401", "Unauthorized");
                AddResponse("404", "Card not found");
                break;

            case "SoftDeleteCard":
                operation.Summary = "Marks card as inactive by ID";
                operation.Description = "Marks an existing card as inactive in the system for the authenticated user.";
                AddResponse("204", "Card marked as inactive");
                AddResponse("400", "Invalid ID format");
                AddResponse("401", "Unauthorized");
                AddResponse("404", "Card not found");
                break;

            case "HardDeleteCard":
                operation.Summary = "Deletes card from database by ID";
                operation.Description = "Permanently deletes a card record from the database for the authenticated user.";
                AddResponse("200", "Card deleted from database");
                AddResponse("400", "Invalid ID format");
                AddResponse("401", "Unauthorized");
                AddResponse("404", "Card not found");
                break;

            default:
                break;
        }
    }
}