namespace FinancialsNice.Application.Helpers;

public static class MinioHelper
{
    private static readonly string Endpoint = Environment.GetEnvironmentVariable("MINIO_BASE_URL")!;
    private static readonly string BucketName = Environment.GetEnvironmentVariable("MINIO_BUCKET")!;

    public static string GeneratePublicUrl(string objectName)
    {
        return $"{Endpoint}/{BucketName}/{objectName}";
    }
}
