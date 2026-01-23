namespace bidPursuit.Domain.Services;

public interface IBlobStorageService
{
    Task<string> UploadToBlobAsync(Stream data, string fileName, CancellationToken cancellationToken);
    string? GetBlobSasUrl(string? Url);
}
