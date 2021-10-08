using Microsoft.Extensions.Configuration;
using Sistemas.Core.Interfaces;

namespace Sistemas.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //
        private readonly IConfiguration _configuration;

        //Constructor
        public UnitOfWork(IConfiguration configuration )
        {
            _configuration = configuration;
        }
        public IUnitOfWorkAdapter Create()
        {
            var connectionString = _configuration.GetConnectionString("Sistemas");

            //var connectionString = _configuration == null
            //   ? Parameters.ConnectionString
            //   : _configuration.GetValue<string>("SqlConnectionString");
            return new UnitOfWorkAdapter(connectionString);
        }
    }
}
