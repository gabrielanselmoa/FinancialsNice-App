using FinancialsNice.Application.Dtos.Minio;
using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;

namespace FinancialsNice.Application.Services;

public class MinioService : IMinioService
{
    private readonly string _bucketName = Environment.GetEnvironmentVariable("MINIO_BUCKET")!;
    private readonly int _expirySeconds = int.Parse(Environment.GetEnvironmentVariable("MINIO_EXPIRYTIME")!);
    private readonly string _baseUrl = Environment.GetEnvironmentVariable("MINIO_BASE_URL")!;

    private static readonly string[] AllowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".pdf" };
    private static readonly string[] AllowedMimeTypes = new[] { "image/png", "image/jpeg", "application/pdf" };
    private const int MaxFileNameLength = 100;
    
    private readonly IMinioClient _minioClient;

    public MinioService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var objectName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
        if (!found)
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));

        await using var stream = file.OpenReadStream();
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType);

        await _minioClient.PutObjectAsync(putObjectArgs);
        return MinioHelper.GeneratePublicUrl(objectName);
    }

    // GENERATES PUT PRESIGNED URL FOR UPLOAD
    public async Task<ResponseModel<UrlResponse>> GenerateUploadUrl(UploadRequest request)
    {
        var response = new ResponseModel<UrlResponse>();
        if (string.IsNullOrWhiteSpace(request.FileName) || request.FileName.Length > MaxFileNameLength)
            return response.Fail(null, "File name is required and must be less than 100 characters long!");

        var fileExtension = Path.GetExtension(request.FileName).ToLower();

        if (!AllowedExtensions.Contains(fileExtension))
            return response.Fail(null, "This file extension is not allowed!");
        
        if (!AllowedMimeTypes.Contains(request.ContentType))
            return response.Fail(null, "This file content-type is not allowed!");

        bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
        if (!found) 
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
        
        var uploadUrl = await _minioClient.PresignedPutObjectAsync(new PresignedPutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(request.FileName)
            .WithExpiry(_expirySeconds));
        
        var fileUrl = $"http://localhost:9000/{_bucketName}/{request.FileName}";
        var result = new UrlResponse()
        {
            UploadUrl = uploadUrl,
            FileUrl = fileUrl
        };
        return response.Ok(result, "Upload url is valid!");
    } 
    
    // GENERATES GET PRESIGNED URL FOR DOWNLOAD
    public async Task<ResponseModel<string>> GenerateDownloadUrl(string fileName)
    {
        var response = new ResponseModel<string>();
        var downloadUrl = await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(fileName)
            .WithExpiry(_expirySeconds));

        return response.Ok(downloadUrl, "Download url is valid!");
    }
}