using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Niwa.Dtos.FileDtos;
using Niwa.Options;

namespace Niwa.Services;

public class FileUploadService(
    IMinioClient client,
    IOptionsMonitor<MinIoOptions> optionsMonitor,
    ILogger<FileUploadService> logger)
    : IFileUploadService
{
    public async Task<List<NoteFileQueryDto>> UploadFilesAsync(IEnumerable<IFormFile> formFileCollection)
    {
        var bucket = optionsMonitor.CurrentValue.Bucket;
        var args = new BucketExistsArgs().WithBucket(bucket);
        args.IsBucketCreationRequest = true;
        var bucketExists = await client.BucketExistsAsync(args);
        if (!bucketExists) await client.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucket));
        var list = new List<NoteFileQueryDto>();
        foreach (var formFile in formFileCollection)
        {
            var filename = formFile.FileName + "-" + Guid.NewGuid();
            await client.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucket)
                .WithObject(filename)
                .WithObjectSize(formFile.Length)
                .WithContentType(formFile.ContentType)
                .WithStreamData(formFile.OpenReadStream()));
            list.Add(new NoteFileQueryDto
            {
                Filename = formFile.FileName,
                FileUrl = filename
            });
            logger.LogInformation("Uploaded file {filename}, size={filesize}", filename, formFile.Length);
        }

        return list;
    }
}