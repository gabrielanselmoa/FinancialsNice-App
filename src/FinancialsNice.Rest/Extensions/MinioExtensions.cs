using Minio;

namespace FinancialsNice.Rest.Extensions;

public static class MinioExtensions
{
    public static IServiceCollection AddMinioClient(this IServiceCollection services)
    {
        var minioUsername = Environment.GetEnvironmentVariable("MINIO_USERNAME")!;
        var minioPassword = Environment.GetEnvironmentVariable("MINIO_PASSWORD")!;
        var minioPort = int.TryParse(Environment.GetEnvironmentVariable("MINIO_PORT"), out int parsedPort) ? parsedPort : 9000;

        services.AddSingleton<IMinioClient>(_ =>
            new MinioClient()
                .WithEndpoint("localhost", minioPort)
                .WithCredentials(minioUsername, minioPassword)
                .Build());

        return services;
    }
}