using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace MovieApi.Storage
{
    public class AzureStorageService : IFileStorage
    {

        private readonly string _azureConnectionString;


        public AzureStorageService(IConfiguration configuration)
        {
            _azureConnectionString = configuration.GetConnectionString("AzureStorage")!;
        }

        public async Task Delete(string container, string path)
        {
            if (string.IsNullOrEmpty(path)) { return; }
            var client = new BlobContainerClient(_azureConnectionString, container);
            await client.CreateIfNotExistsAsync();
            var file = Path.GetFileName(path);
            var blob = client.GetBlobClient(file);
            await blob.DeleteIfExistsAsync();


        }

        public async Task<string> Edit(byte[] content, string extention, string container, string contentType, string path)
        {

            await Delete(container,path);
            return await Save(content, extention, container, contentType);

        }

        public async Task<string> Save(byte[] content, string extention, string container, string contentType)
        {
            var client = new BlobContainerClient(_azureConnectionString, container);
            await client.CreateIfNotExistsAsync();
            client.SetAccessPolicy(PublicAccessType.Blob);

            var fileName = $"{Guid.NewGuid()}{extention}";
            var blob = client.GetBlobClient(fileName);

            var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHeader = new BlobHttpHeaders
            {
                ContentType = contentType
            };
            blobUploadOptions.HttpHeaders = blobHttpHeader;

            await blob.UploadAsync(new BinaryData(content), blobUploadOptions);
            return blob.Uri.ToString();


        }
    }
}
