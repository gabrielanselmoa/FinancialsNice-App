using FinancialsNice.Application.Dtos.Minio;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialsNice.Rest.Controllers;

[Authorize]
[ApiController]
[Route("files")]
public class MinioController(IMinioService minioService, ILogger<MinioController> logger)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadFile(
        [FromForm] MinioRequest request)
    {
        try
        {
            if (request.File == null || request.File.Length == 0)
            {
                logger.LogWarning("UploadFile request received with no file or empty file.");
                return StatusCode(400, "No file uploaded");
            }

            var url = await minioService.UploadFileAsync(request.File);
            return StatusCode(200, new { url });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error uploading file.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> Upload(
        [FromBody] UploadRequest request)
    {
        try
        {
            var response = await minioService.GenerateUploadUrl(request);
            return StatusCode(200, response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error generating upload URL.");
            return StatusCode(400, e.Message);
        }
    }

    [HttpGet]
    [Route("download/{fileName}")]
    public async Task<IActionResult> Download(
        [FromRoute] string fileName)
    {
        try
        {
            var response = await minioService.GenerateDownloadUrl(fileName);
            if (response.Data == null)
            {
                logger.LogWarning("File with name {FileName} not found for download URL generation.", fileName);
                return StatusCode(404, $"File with name {fileName} not found.");
            }

            return StatusCode(200, response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error generating download URL for file: {FileName}", fileName);
            return StatusCode(400, e.Message);
        }
    }
}