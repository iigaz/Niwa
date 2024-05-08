using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Niwa.Models;
using Niwa.Options;

namespace Niwa.Services;

public class FileDownloadService(
    IMinioClient client,
    IOptionsMonitor<MinIoOptions> optionsMonitor,
    ILogger<FileDownloadService> logger)
    : IFileDownloadService
{
    public async Task<string> GetDownloadUrlAsync(NoteFile noteFile)
    {
        var bucket = optionsMonitor.CurrentValue.Bucket;
        var presigned = await client.PresignedGetObjectAsync(new PresignedGetObjectArgs().WithBucket(bucket)
            .WithObject(noteFile.FileUrl).WithExpiry(60 * 60 * 24));
        logger.LogInformation("Created download string for file {filename}, expires in 24 hours.", noteFile.FileUrl);
        return presigned;
    }
}