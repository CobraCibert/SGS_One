using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sistemas.UI.Models;
using Sistemas.UI.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistemas.UI.Extensions
{
    public static class AddFormsHostBuilderExtensions
    {
        public static IHostBuilder AddForms(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<Acesso>();
                services.AddSingleton<MDI>();
                services.AddTransient<Bancos>();
            });

            return host;
        }
    }
}
