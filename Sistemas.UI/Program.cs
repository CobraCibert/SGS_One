using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sistemas.UI.Extensions;
using Sistemas.UI.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistemas.UI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Injectamos los services, formularios y conexion
            var host = Host.CreateDefaultBuilder()
                .AddServices()
                .AddForms()
                .Build();
            var services = host.Services;
            var mainForm = services.GetRequiredService<Acesso>();
            Application.Run(mainForm);

            //Application.Run(new Form1());
        }
    }
}
