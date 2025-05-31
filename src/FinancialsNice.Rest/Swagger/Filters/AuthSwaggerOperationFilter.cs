using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class AuthSwaggerOperationFilter : IOperationFilter
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
            case "SignUp":
                operation.Summary = "User registration";
                operation.Description = "Registers a new user account.";
                AddResponse("200", "User registered successfully");
                AddResponse("400", "Invalid request data or registration failed");
                break;

            case "SignIn":
                operation.Summary = "User login";
                operation.Description = "Authenticates a user and returns authentication tokens.";
                AddResponse("200", "User authenticated successfully");
                AddResponse("400", "Invalid credentials or login failed");
                break;

            case "ValidateToken":
                operation.Summary = "Validate authentication token";
                operation.Description = "Checks if a provided authentication token is valid.";
                AddResponse("200", "Token validation result");
                AddResponse("400", "Invalid token format or validation failed");
                break;

            case "ForgotPassword":
                operation.Summary = "Request password reset";
                operation.Description = "Initiates the password reset process for a user.";
                AddResponse("200", "Password reset accepted");
                AddResponse("400", "Invalid email or request failed");
                break;

            case "ResetPassword":
                operation.Summary = "Reset password";
                operation.Description = "Resets the user's password using a token.";
                AddResponse("200", "Password updated!");
                AddResponse("400", "Invalid token, password, or reset failed");
                break;

            case "ConfirmEmail":
                operation.Summary = "Confirm email address";
                operation.Description = "Confirms a user's email address using a token.";
                AddResponse("200", "Email confirmation result");
                AddResponse("401", "Unauthorized. User ID not found in claims."); // Matches controller's 401
                AddResponse("400", "Confirmation failed");
                break;

            default:
                break;
        }
    }
}