using Shared;

namespace Application.Services;

public record FileResponse(Stream Stream, string ContentType);

public interface IStorageService
{
    Task<Result> DeleteAsync(string fileName, string containerName, CancellationToken cancellationToken = default);

    Task<Result<FileResponse>> DownloadAsync(string fileName, string containerName, CancellationToken cancellationToken = default);

    Task<Result> CopyTempToAsync(string fileName, string containerName, CancellationToken cancellationToken = default);

    Result<Uri> GenerateSasUriTempContainer();

    Result<Uri> GetSignedUri(string fileName, string containerName, bool isDownload = false);
}
