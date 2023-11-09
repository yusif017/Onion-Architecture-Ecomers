using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Ecomerce.Application.Abstraction.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace Ecomerce.Infrastructure.Concreate.Storages.Azure;

public class AzureStorage : Storage, IAzureStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    private BlobContainerClient _containerClient;

    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new BlobServiceClient(configuration["Storage:Azure"]);
    }

    public async Task DeleteAsync(string containerName, string fileName)
    {
        _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = _containerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync();
    }

    public List<string> GetAllFiles(string containerName)
    {
        _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _containerClient.GetBlobs().Select(x => x.Name).ToList();
    }

    public bool HasFile(string containerName, string fileName)
    {
        _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _containerClient.GetBlobs().Any(x => x.Name == fileName);
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
    {
        _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        // Check if the container exists, and create it if not
        await _containerClient.CreateIfNotExistsAsync();

        // Check if public access is already set
        BlobContainerProperties properties = await _containerClient.GetPropertiesAsync();
        if (properties.PublicAccess != PublicAccessType.Blob)
        {
            // Set public access for blobs if it's not set
            await _containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
        }

        List<(string fileName, string pathOrContainerName)> datas = new();
        foreach (IFormFile file in files)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(file.Name);
            await blobClient.UploadAsync(file.OpenReadStream());
            datas.Add((file.Name, containerName));
        }

        return datas;
    }
}
