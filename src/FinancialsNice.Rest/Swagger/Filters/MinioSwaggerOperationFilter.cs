using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialsNice.Rest.Swagger.Filters;

public class MinioSwaggerOperationFilter : IOperationFilter
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
            case "UploadFile":
                operation.Summary = "Uploads a file";
                operation.Description = "Uploads a file to the Minio storage.";
                AddResponse("200", "File uploaded successfully");
                AddResponse("400", "No file uploaded or upload failed");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            case "Upload":
                operation.Summary = "Generates a pre-signed upload URL";
                operation.Description = "Generates a URL for direct file upload to Minio.";
                AddResponse("200", "Upload URL generated successfully");
                AddResponse("400", "Invalid request data or URL generation failed");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                break;

            case "Download":
                operation.Summary = "Generates a pre-signed download URL";
                operation.Description = "Generates a URL for direct file download from Minio.";
                AddResponse("200", "Download URL generated successfully");
                AddResponse("400", "Invalid file name or URL generation failed");
                AddResponse("401", "Unauthorized. The user is not authenticated.");
                AddResponse("404", "File not found for the given file name.");
                break;

            default:
                break;
        }
    }
}