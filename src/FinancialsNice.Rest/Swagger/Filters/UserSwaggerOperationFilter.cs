using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class UserSwaggerOperationFilter : IOperationFilter
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
            case "GetAllUsers":
                operation.Summary = "Lists all users with pagination";
                operation.Description =
                    "Provides a paginated list of all users. This endpoint typically requires administrative privileges.";
                AddResponse("200", "List of users returned successfully");
                AddResponse("400", "Invalid request parameters.");
                AddResponse("401", "Unauthorized. Requires authentication."); // Added as [Authorize] is active
                break;

            case "GetUserById":
                operation.Summary = "Gets a user by ID";
                operation.Description =
                    "Retrieves a specific user by their ID. This endpoint typically requires administrative privileges or accessing one's own data.";
                AddResponse("200", "Owner returned successfully");
                AddResponse("400", "Invalid ID format or request failed.");
                AddResponse("401", "Unauthorized. Requires authentication.");
                AddResponse("404", "Owner not found for the provided ID.");
                break;

            case "GetLoggedUser":
                operation.Summary = "Gets the currently logged-in user's details";
                operation.Description =
                    "Retrieves the full user details based on the claims of the authenticated user.";
                AddResponse("200", "Logged-in user details returned successfully");
                AddResponse("401", "Unauthorized. Owner is not authenticated.");
                AddResponse("404", "Owner details not found for the logged-in user ID.");
                AddResponse("400", "Error fetching logged-in user details.");
                break;

            case "GetUserByEmail":
                operation.Summary = "Gets a user by email address";
                operation.Description =
                    "Retrieves a specific user by their email address. This endpoint typically requires administrative privileges or a specific purpose allowing lookup by email.";
                AddResponse("200", "Owner returned successfully");
                AddResponse("400", "Invalid email format or request failed.");
                AddResponse("401", "Unauthorized. Requires authentication.");
                AddResponse("404", "Owner not found for the provided email.");
                break;

            case "UpdateUser":
                operation.Summary = "Updates the currently logged-in user's profile";
                operation.Description = "Updates the authenticated user's own record based on the provided data.";

                AddResponse("200", "Owner updated successfully");
                AddResponse("400", "Invalid request data or update failed.");
                AddResponse("401", "Unauthorized. Owner is not authenticated.");
                AddResponse("404", "Logged-in user not found or could not be updated.");
                break;

            case "SoftDeleteUser":
                operation.Summary = "Soft deletes a user by ID";
                operation.Description =
                    "Marks an existing user as logically deleted. This endpoint typically requires administrative privileges.";
                AddResponse("204", "Owner marked as deleted (soft delete) successfully");
                AddResponse("400", "Invalid ID format or deletion failed.");
                AddResponse("401", "Unauthorized. Requires authentication.");
                AddResponse("404", "Owner not found for the provided ID or could not be soft-deleted.");
                break;

            case "HardDeleteUser":
                operation.Summary = "Hard deletes a user by ID";
                operation.Description =
                    "Permanently deletes a user record from the database. This is an irreversible operation and typically requires high administrative privileges.";
                AddResponse("204", "Owner permanently deleted (hard delete) successfully");
                AddResponse("400", "Invalid ID format or deletion failed.");
                AddResponse("401", "Unauthorized. Requires authentication.");
                AddResponse("404", "Owner not found for the provided ID or could not be hard-deleted.");
                break;

            default:
                break;
        }
    }
}