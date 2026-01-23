using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using bidPursuit.Domain.Services;
using bidPursuit.Infrastructure.Persistence.Configurations;
using Microsoft.Extensions.Options;

namespace bidPursuit.Infrastructure.Services;

internal class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;
    public async Task<string> UploadToBlobAsync(Stream data, string fileName, CancellationToken cancellationToken)
    {
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.ContainerName);

        var blobClinet = containerClient.GetBlobClient(fileName);

        var headers = new BlobHttpHeaders
        {
            ContentType = GetContentType(fileName)
        };

        await blobClinet.UploadAsync(
            data,
            new BlobUploadOptions
            {
                HttpHeaders = headers
            },
            cancellationToken
        );

        return blobClinet.Uri.ToString();
    }


    public string? GetBlobSasUrl(string? blobUrl)
    {
        if (blobUrl is null) return null;

        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = _blobStorageSettings.ContainerName,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(3),
            BlobName = GetBlobNameFromUrl(blobUrl)
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);

        var sasToken = sasBuilder
            .ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobServiceClient.AccountName, _blobStorageSettings.AccountKey))
            .ToString();

        return $"{blobUrl}?{sasToken}";
    }

    private string GetBlobNameFromUrl(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        return uri.Segments.Last();
    }

    private string GetContentType(string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return ext switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            _ => "application/octet-stream" // fallback
        };
    }
}
