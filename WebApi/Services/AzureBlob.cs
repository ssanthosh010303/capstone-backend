/*
 * Author: Apache X692 Attack Helicopter
 * Created: 25/07/2021
 */
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

using WebApi.Models.DataTransferObjects;

namespace WebApi.Services;

public interface IAzureBlobService
{
    Dictionary<string, string> GetUploadUrls(AzureBlobGetUploadUrlsDto dtoEntity);

    string GetDownloadUrl(string blobName);
}

public class AzureBlobService(
    IConfiguration configuration, SecretClient secretClient) : IAzureBlobService
{
    private readonly BlobServiceClient _blobServiceClient = new(secretClient
        .GetSecret("AzureBlobStorageConnectionString").Value.Value
    );
    private readonly string _containerName = configuration["AZURE_STORAGE_ACCOUNT"]!;

    public Dictionary<string, string> GetUploadUrls(AzureBlobGetUploadUrlsDto dtoEntity)
    {
        Dictionary<string, string> uploadUrls = [];

        foreach (var fileExtension in dtoEntity.FileExtensions)
        {
            string blobName = GenerateBlobName(
                dtoEntity.SubmittedWithGrievanceId, fileExtension
            );

            BlobClient blobClient = _blobServiceClient.GetBlobContainerClient(
                _containerName).GetBlobClient(blobName);
            BlobSasBuilder sasBuilder = new()
            {
                BlobContainerName = _containerName,
                BlobName = blobName,
                Resource = "b",
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30)
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Write);

            var sasUri = blobClient.GenerateSasUri(sasBuilder);
            uploadUrls.Add(blobName, sasUri.ToString());
        }

        return uploadUrls;
    }

    public string GetDownloadUrl(string blobName)
    {
        var blobClient = _blobServiceClient.GetBlobContainerClient(_containerName)
            .GetBlobClient(blobName);

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = _containerName,
            BlobName = blobName,
            Resource = "b",
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30)
        };
        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        return blobClient.GenerateSasUri(sasBuilder).ToString();
    }

    private static string GenerateBlobName(int responseId, string fileExtension)
    {
        return $"{responseId}/{Guid.NewGuid()}.{fileExtension}";
    }
}
