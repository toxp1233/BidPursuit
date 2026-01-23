namespace bidPursuit.Infrastructure.Persistence.Configurations;

public class BlobStorageSettings
{
    public string ConnectionString { get; set; } = default!;
    public string ContainerName { get; set; } = default!;
    public string AccountKey { get; set; } = default!;
}
