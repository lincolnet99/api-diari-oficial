using Azure.Storage.Blobs;
using Regulatorio.Domain.AzureStorage;

namespace Regulatorio.Infra.Repository.AzureStorage.Normativos
{
    public class NormativoFileStorageRepository : FileStorageBaseRepository, IFileStorageRepository
    {
        private const string NormativoContainerName = "regulatorio-arquivo-normativo";
        private readonly BlobServiceClient _blobServiceClient;

        public NormativoFileStorageRepository()
        {
            _blobServiceClient = new BlobServiceClient(new Uri(ContainerConnectionString));
        }

        public async Task DeleteFileAsync(string blobName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(NormativoContainerName);

            var blobClient = blobContainerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<MemoryStream> GetFileAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(NormativoContainerName);

            var blobClient = containerClient.GetBlobClient(blobName);

            MemoryStream memoryStream = new();

            await blobClient.DownloadToAsync(memoryStream);

            memoryStream.Position = 0;

            return memoryStream;
        }

        public async Task<string> UploadFileAsync(string blobName, Stream fileStream)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(NormativoContainerName);

            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(fileStream, overwrite: true);

            return blobClient.Uri.GetLeftPart(UriPartial.Path);
        }
    }
}
