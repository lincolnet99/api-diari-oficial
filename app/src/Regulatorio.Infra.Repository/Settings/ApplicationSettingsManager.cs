using Regulatorio.Core;
using Regulatorio.Core.infrastructure;

namespace Regulatorio.Infra.Repository.Settings
{
    public static class ApplicationSettingsManager
    {
        public static AppSettings LoadSettings()
        {
            var settings = Singleton<AppSettings>.Instance;

            if (settings == null)
            {
                return new AppSettings();
            }

            return Singleton<AppSettings>.Instance;
        }
    }
}