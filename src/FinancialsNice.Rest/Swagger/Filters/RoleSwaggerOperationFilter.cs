using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class RoleSwaggerOperationFilter : IOperationFilter
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
            case "GetAllRoles":
                operation.Summary = "Lists all roles";
                operation.Description = "Retrieves a list of all available roles.";
                AddResponse("200", "List returned successfully");
                AddResponse("400", "Error fetching roles.");
                break;

            case "GetRoleById":
                operation.Summary = "Gets a role by ID";
                operation.Description = "Retrieves a specific role by its ID.";
                AddResponse("200", "Role returned successfully");
                AddResponse("400", "Invalid ID format or request failed.");
                AddResponse("404", "Role not found for the provided ID.");
                break;

            case "CreateRole":
                operation.Summary = "Creates a role";
                operation.Description = "Registers a new role in the system.";
                AddResponse("201", "Role created successfully");
                AddResponse("400", "Invalid request data or creation failed.");
                break;

            case "UpdateRole":
                operation.Summary = "Updates a role by ID";
                operation.Description = "Updates an existing role record based on its ID.";
                AddResponse("200", "Role updated successfully");
                AddResponse("400", "Invalid request data or incorrect ID format.");
                AddResponse("404", "Role not found for the provided ID.");
                break;

            case "SoftDeleteRole":
                operation.Summary = "Marks role as inactive by ID";
                operation.Description = "Marks an existing role as inactive in the system.";
                AddResponse("204", "Role marked as inactive successfully");
                AddResponse("400", "Invalid ID format or deletion failed.");
                AddResponse("404", "Role not found for the provided ID or could not be soft-deleted.");
                break;

            case "HardDeleteRole":
                operation.Summary = "Deletes role from database by ID";
                operation.Description = "Permanently deletes a role record from the database.";
                AddResponse("204", "Role deleted from database successfully");
                AddResponse("400", "Invalid ID format or deletion failed.");
                AddResponse("404", "Role not found for the provided ID or could not be hard-deleted.");
                break;

            default:
                break;
        }
    }
}