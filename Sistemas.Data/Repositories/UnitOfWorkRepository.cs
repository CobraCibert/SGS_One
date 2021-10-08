using Sistemas.Core.Interfaces;
using Sistemas.Core.Interfaces.Models;
using Sistemas.Data.Repositories.Models;
using System.Data.SqlClient;

namespace Sistemas.Data.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public IBancoRepository BancoRepository { get; }
        //Constructor
        public UnitOfWorkRepository(SqlConnection context, SqlTransaction transaction)
        {
            BancoRepository = new BancoRepository(context, transaction);
        }
    }
}
