using Minio;
using Minio.DataModel.Args;
using Niwa.Models;

namespace Niwa.Services;

public class FileDownloadService(IMinioClient client, IConfiguration configuration, ILogger<FileDownloadService> logger)
    : IFileDownloadService
{
    public async Task<string> GetDownloadUrlAsync(NoteFile noteFile)
    {
        var bucket = configuration["MinIO:Bucket"];
        var presigned = await client.PresignedGetObjectAsync(new PresignedGetObjectArgs().WithBucket(bucket)
            .WithObject(noteFile.FileUrl).WithExpiry(60 * 60 * 24));
        logger.LogInformation("Created download string for file {filename}, expires in 24 hours.", noteFile.FileUrl);
        return presigned;
    }
}