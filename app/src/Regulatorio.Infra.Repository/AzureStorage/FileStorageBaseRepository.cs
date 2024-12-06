using Regulatorio.Infra.Repository.Settings;

namespace Regulatorio.Infra.Repository.AzureStorage
{
    public abstract class FileStorageBaseRepository
    {
        protected static string ContainerConnectionString => ApplicationSettingsManager.LoadSettings().NormativoContainerConnectionString;
    }
}
