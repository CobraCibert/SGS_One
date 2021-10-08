using Microsoft.Extensions.DependencyInjection;
using Sistemas.Core.Interfaces;
using Sistemas.Core.Services.Interfaces;
using Sistemas.Core.Services.Repositories;
using Sistemas.Data.Repositories;

namespace Sistemas.Data.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IBancoService, BancoService>();

            return services;
        }
    }
}
