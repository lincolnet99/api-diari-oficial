namespace Regulatorio.Domain.AzureStorage
{
    public interface IFileStorageRepository
    {
        Task<string> UploadFileAsync(string blobName, Stream fileStream);
        Task DeleteFileAsync(string blobName);
        Task<MemoryStream> GetFileAsync(string blobName);
    }
}
