using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class PayerReceiverSwaggerOperationFilter : IOperationFilter
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
            case "GetAllPayerReceivers":
                operation.Summary = "Lists all payers/receivers with pagination and search";
                operation.Description =
                    "Provides a list of all payers or receivers with pagination, optionally filtered by a search term and type.";
                AddResponse("200", "List returned successfully");
                AddResponse("400", "Invalid request parameters or error retrieving data");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            case "GetPayerReceiverById":
                operation.Summary = "Gets a payer/receiver by Id";
                operation.Description = "Retrieves a specific payer or receiver by their Id.";
                AddResponse("200", "Payer/Receiver returned successfully");
                AddResponse("400", "Invalid request or incorrect ID format");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                AddResponse("404", "Payer/Receiver not found for the provided ID.");
                break;

            case "CreatePayerReceiver":
                operation.Summary = "Creates a payer/receiver";
                operation.Description = "Registers a new payer or receiver in the system.";
                AddResponse("201", "Payer/Receiver created successfully"); // Changed to 201 to reflect controller
                AddResponse("400", "Invalid request data or creation failed.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            case "UpdatePayerReceiver":
                operation.Summary = "Updates a payer/receiver by Id";
                operation.Description = "Updates an existing payer or receiver record based on their Id.";
                AddResponse("200", "Payer/Receiver updated successfully");
                AddResponse("400", "Invalid request data or incorrect ID format.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                AddResponse("404", "Payer/Receiver not found for the provided ID.");
                break;

            case "SoftDeletePayerReceiver":
                operation.Summary = "Marks payer/receiver as inactive by ID";
                operation.Description = "Marks an existing payer or receiver as inactive in the system.";
                AddResponse("204", "Payer/Receiver marked as inactive successfully"); // Changed to 204
                AddResponse("400", "Invalid ID format.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                AddResponse("404", "Payer/Receiver not found for the provided ID or could not be soft-deleted.");
                break;

            case "HardDeletePayerReceiver":
                operation.Summary = "Deletes payer/receiver from database by ID";
                operation.Description = "Permanently deletes a payer or receiver record from the database.";
                AddResponse("204", "Payer/Receiver deleted from database successfully"); // Changed to 204
                AddResponse("400", "Invalid ID format.");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                AddResponse("404", "Payer/Receiver not found for the provided ID or could not be hard-deleted.");
                break;

            default:
                break;
        }
    }
}