using Microsoft.AspNetCore.Http;

namespace FinancialsNice.Application.Dtos.Minio;

public record MinioRequest(IFormFile? File);