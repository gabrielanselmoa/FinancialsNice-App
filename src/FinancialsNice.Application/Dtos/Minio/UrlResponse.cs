namespace FinancialsNice.Application.Dtos.Minio;

public record UrlResponse
{
    public string UploadUrl { get; init; } = null!;
    public string FileUrl { get; init; } = null!;
}