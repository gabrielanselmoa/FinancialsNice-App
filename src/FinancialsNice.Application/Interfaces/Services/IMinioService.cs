using FinancialsNice.Application.Dtos.Minio;
using FinancialsNice.Application.Dtos.ResultPattern;
using Microsoft.AspNetCore.Http;

namespace FinancialsNice.Application.Interfaces.Services;

public interface IMinioService
{
    Task<string> UploadFileAsync(IFormFile file);
    Task<ResponseModel<UrlResponse>> GenerateUploadUrl(UploadRequest request);
    Task<ResponseModel<string>> GenerateDownloadUrl(string fileName);
} 