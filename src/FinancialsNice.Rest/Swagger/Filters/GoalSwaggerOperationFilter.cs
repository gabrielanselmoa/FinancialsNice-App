using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class GoalSwaggerOperationFilter : IOperationFilter
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
            case "GetAllGoals":
                operation.Summary = "Lists all goals with pagination";
                operation.Description = "Provides a list of all goals with pagination.";
                AddResponse("200", "List returned successfully");
                AddResponse("400", "Invalid request");
                AddResponse("401", "Unauthorized");
                break;

            case "GetGoalById":
                operation.Summary = "Gets a goal by Id";
                operation.Description = "Retrieves a specific goal by its Id.";
                AddResponse("200", "Goal returned successfully");
                AddResponse("400", "Invalid request");
                AddResponse("401", "Unauthorized");
                AddResponse("404", "Goal not found");
                break;

            case "CreateGoal":
                operation.Summary = "Creates a goal";
                operation.Description = "Registers a new goal in the system.";
                AddResponse("201", "Goal created successfully");
                AddResponse("400", "Invalid request data");
                AddResponse("401", "Unauthorized");
                break;

            case "UpdateGoal":
                operation.Summary = "Updates a goal by Id";
                operation.Description = "Updates an existing goal record based on its Id.";
                AddResponse("200", "Goal updated successfully");
                AddResponse("400", "Invalid request data or ID format");
                AddResponse("401", "Unauthorized");
                AddResponse("404", "Goal not found");
                break;

            case "SoftDeleteGoal":
                operation.Summary = "Marks goal as inactive by ID";
                operation.Description = "Marks an existing goal as inactive in the system.";
                AddResponse("204", "Goal marked as inactive");
                AddResponse("400", "Invalid ID");
                AddResponse("401", "Unauthorized");
                AddResponse("404", "Goal not found");
                break;

            case "HardDeleteGoal":
                operation.Summary = "Deletes goal from database by ID";
                operation.Description = "Permanently deletes a goal record from the database.";
                AddResponse("204", "Goal deleted from database");
                AddResponse("400", "Invalid ID");
                AddResponse("401", "Unauthorized");
                AddResponse("404", "Goal not found");
                break;

            default:
                break;
        }
    }
}