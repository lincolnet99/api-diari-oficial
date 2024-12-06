using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Regulatorio.ApplicationService.Services.Contatos;
using Regulatorio.ApplicationService.Services.Dominios;
using Regulatorio.ApplicationService.Services.Filtros;
using Regulatorio.ApplicationService.Services.Garantias;
using Regulatorio.ApplicationService.Services.InstituicaoFinanceiras;
using Regulatorio.ApplicationService.Services.Normativos;
using Regulatorio.ApplicationService.Services.PalavrasChave;
using Regulatorio.ApplicationService.Services.Registradoras;
using Regulatorio.ApplicationService.Services.Registros;
using Regulatorio.Domain.Services.Contatos;
using Regulatorio.Domain.Services.Dominios;
using Regulatorio.Domain.Services.Filtros;
using Regulatorio.Domain.Services.Garantias;
using Regulatorio.Domain.Services.InstituicaoFinanceira;
using Regulatorio.Domain.Services.Normativos;
using Regulatorio.Domain.Services.PalavrasChave;
using Regulatorio.Domain.Services.Registradoras;
using Regulatorio.Domain.Services.Registros;

namespace Regulatorio.ApplicationService
{
    public static class ApplicationServicesExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddApplicationServices();
        }

        private static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDominioService, DominioAppService>();
            services.AddScoped<INormativoService, NormativoAppService>();
            services.AddScoped<IGarantiaService, GarantiaAppService>();
            services.AddScoped<IRegistroService, RegistroAppService>();
            services.AddScoped<IFiltroService, FiltroAppService>();
            services.AddScoped<IInstituicaoFinanceiraService, InstituicaoFinanceiraAppService>();
            services.AddScoped<IRegistradorasService, RegistradorasAppService>();
            services.AddScoped<IContatoService, ContatoAppService>();
            services.AddScoped<IDiarioOficialService, DiarioOficialAppService>();
        }
    }
}
