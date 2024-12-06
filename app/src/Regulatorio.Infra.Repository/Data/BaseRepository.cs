using Regulatorio.Infra.Repository.Settings;

namespace Regulatorio.Infra.Repository.Data
{
    public abstract class BaseRepository
    {
        protected static string ConnectionString => ApplicationSettingsManager.LoadSettings().RegulatorioDatabaseServer;

    }
}
