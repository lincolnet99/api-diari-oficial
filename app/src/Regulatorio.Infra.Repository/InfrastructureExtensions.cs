using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Regulatorio.Core;
using Regulatorio.Core.infrastructure;
using Regulatorio.Domain.AzureStorage;
using Regulatorio.Domain.Repositories.Contatos;
using Regulatorio.Domain.Repositories.Dominios;
using Regulatorio.Domain.Repositories.Filtros;
using Regulatorio.Domain.Repositories.Garantias;
using Regulatorio.Domain.Repositories.InstituicaoFinanceiraRepository;
using Regulatorio.Domain.Repositories.Normativos;
using Regulatorio.Domain.Repositories.PalavrasChave;
using Regulatorio.Domain.Repositories.RegistradoraRepository;
using Regulatorio.Domain.Repositories.Registros;
using Regulatorio.Infra.Repository.AzureStorage.Normativos;
using Regulatorio.Infra.Repository.Data;
using Regulatorio.Infra.Repository.Data.Dominios;
using Regulatorio.Infra.Repository.Data.Filtros;
using Regulatorio.Infra.Repository.Data.Garantias;
using Regulatorio.Infra.Repository.Data.InstituicaoFinanceira;
using Regulatorio.Infra.Repository.Data.Normativos;
using Regulatorio.Infra.Repository.Data.PalavrasChave;
using Regulatorio.Infra.Repository.Data.RegistradorasRepository;
using Regulatorio.Infra.Repository.Data.Registros;

namespace Regulatorio.Infra.Repository
{
    public static class InfrastructureExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddRepositories();
            services.AddFilesRepositories();
            services.AddAppSettings(config);
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IDominioRepository, DominioRepository>();
            services.AddTransient<INormativoRepository, NormativoRepository>();
            services.AddTransient<IGarantiaRepository, GarantiaRepository>();
            services.AddTransient<IRegistroRepository, RegistroRepository>();
            services.AddTransient<IInstituicaoFinanceiraRepository, InstituicaoFinanceiraRepository>();
            services.AddTransient<IFiltroRepository, FiltroRepository>();
            services.AddTransient<IRegistradoraRepository, RegistradorasRepository>();
            services.AddTransient<IContatoRepository, ContatoRepository>();
            services.AddTransient<IDiarioOficialRepository, DiarioOficialRepository>();
        } 

        private static void AddFilesRepositories(this IServiceCollection services)
        {
            services.AddTransient<IFileStorageRepository, NormativoFileStorageRepository>();
        }

        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("Regulatorio").Get<AppSettings>();

            Singleton<AppSettings>.Instance = settings;

            return services;
        }
    }
}
