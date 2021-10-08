using Microsoft.Extensions.DependencyInjection;
using Sistemas.UI.Models;
using Sistemas.UI.Utilitarios;

namespace Sistemas.UI.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            //services.AddScoped<IBancoRepository, BancoRepository>();
            services.AddSingleton<Acesso>();
            services.AddSingleton<MDI>();
            services.AddTransient<Bancos>();
            return services;
        }
    }
}
