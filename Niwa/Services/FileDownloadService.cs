using Minio;
using Minio.DataModel.Args;
using Niwa.Models;

namespace Niwa.Services;

public class FileDownloadService(IMinioClient client, IConfiguration configuration) : IFileDownloadService
{
    public async Task<string> GetDownloadUrlAsync(NoteFile noteFile)
    {
        var bucket = configuration["MinIO:Bucket"];
        var presigned = await client.PresignedGetObjectAsync(new PresignedGetObjectArgs().WithBucket(bucket)
            .WithObject(noteFile.FileUrl).WithExpiry(60 * 60 * 24));
        return presigned;
    }
}