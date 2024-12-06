using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Regulatorio.Core.infrastructure;
using Regulatorio.SharedKernel.Common;
using Regulatorio.SharedKernel.Mappers;

namespace Regulatorio.Core
{
    public static class CoreExtensions
    {
        private static ITypeFinder _typeFinder;

        public static void ConfigureCore(this IServiceCollection services, IConfiguration config)
        {
            _typeFinder = new AppDomainTypeFinder();

            AddAutoMapper();
        }

        private static void AddAutoMapper()
        {
            var mapperConfigurations = _typeFinder.FindClassesOfType<Profile>();

            var instances = mapperConfigurations
                .Select(mapperConfiguration => (Profile)Activator.CreateInstance(mapperConfiguration));

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var instance in instances)
                {
                    cfg.AddProfile(instance?.GetType());
                }
            });

            AutoMapperConfiguration.Init(config);
        }
    }
}
