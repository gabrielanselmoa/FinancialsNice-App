using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class AddressSwaggerOperationFilter : IOperationFilter
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
            case "GetAllAddresses":
                operation.Summary = "Lists all addresses with pagination";
                operation.Description = "Provides a list of all addresses with pagination.";
                AddResponse("200", "List returned successfully");
                AddResponse("400", "Invalid request");
                AddResponse("401", "Unauthorized");
                break;

            case "GetAddressById":
                operation.Summary = "Gets an address by Id";
                operation.Description = "Retrieves a specific address by its Id.";
                AddResponse("200", "Address returned successfully");
                AddResponse("400", "Invalid request");
                AddResponse("401", "Unauthorized");
                AddResponse("404", "Address not found");
                break;

            case "CreateAddress":
                operation.Summary = "Creates an address";
                operation.Description = "Registers a new address in the system.";
                AddResponse("201", "Address created successfully");
                AddResponse("400", "Invalid request data");
                AddResponse("401", "Unauthorized");
                break;

            case "UpdateAddress":
                operation.Summary = "Updates an address by Id";
                operation.Description = "Updates an existing address record based on its Id.";
                AddResponse("200", "Address updated successfully");
                AddResponse("400", "Invalid request data or ID format");
                AddResponse("401", "Unauthorized");
                AddResponse("404", "Address not found");
                break;

            case "SoftDeleteAddress":
                operation.Summary = "Marks address as inactive by ID";
                operation.Description = "Marks an existing address as inactive in the system.";
                AddResponse("204", "Address marked as inactive");
                AddResponse("400", "Invalid ID");
                AddResponse("401", "Unauthorized");
                AddResponse("404", "Address not found");
                break;

            case "HardDeleteAddress":
                operation.Summary = "Deletes address from database by ID";
                operation.Description = "Permanently deletes an address record from the database.";
                AddResponse("204", "Address deleted from database");
                AddResponse("400", "Invalid ID");
                AddResponse("401", "Unauthorized");
                AddResponse("404", "Address not found");
                break;

            default:
                break;
        }
    }
}