using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class PermissionSwaggerOperationFilter : IOperationFilter
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
            case "GetAllPermissions":
                operation.Summary = "Lists all permissions";
                operation.Description = "Retrieves a list of all available permissions.";
                AddResponse("200", "List returned successfully");
                AddResponse("400", "Error fetching permissions.");
                break;

            case "GetPermissionById":
                operation.Summary = "Gets a permission by ID";
                operation.Description = "Retrieves a specific permission by its ID.";
                AddResponse("200", "Permission returned successfully");
                AddResponse("400", "Invalid ID format or request failed.");
                AddResponse("404", "Permission not found for the provided ID.");
                break;

            case "CreatePermission":
                operation.Summary = "Creates a permission";
                operation.Description = "Registers a new permission in the system.";
                AddResponse("201", "Permission created successfully");
                AddResponse("400", "Invalid request data or creation failed.");
                break;

            case "UpdatePermission":
                operation.Summary = "Updates a permission by ID";
                operation.Description = "Updates an existing permission record based on its ID.";
                AddResponse("200", "Permission updated successfully");
                AddResponse("400", "Invalid request data or incorrect ID format.");
                AddResponse("404", "Permission not found for the provided ID.");
                break;

            case "SoftDeletePermission":
                operation.Summary = "Marks permission as inactive by ID";
                operation.Description = "Marks an existing permission as inactive in the system.";
                AddResponse("204", "Permission marked as inactive successfully");
                AddResponse("400", "Invalid ID format or deletion failed.");
                AddResponse("404", "Permission not found for the provided ID or could not be soft-deleted.");
                break;

            case "HardDeletePermission":
                operation.Summary = "Deletes permission from database by ID";
                operation.Description = "Permanently deletes a permission record from the database.";
                AddResponse("204", "Permission deleted from database successfully");
                AddResponse("400", "Invalid ID format or deletion failed.");
                AddResponse("404", "Permission not found for the provided ID or could not be hard-deleted.");
                break;

            default:
                break;
        }
    }
}