using Minio;
using Minio.DataModel.Args;
using Niwa.Dtos.FileDtos;

namespace Niwa.Services;

public class FileUploadService(IMinioClient client, IConfiguration configuration) : IFileUploadService
{
    public async Task<List<NoteFileQueryDto>> UploadFiles(IEnumerable<IFormFile> formFileCollection)
    {
        var bucket = configuration["MinIO:Bucket"];
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
        }

        return list;
    }
}