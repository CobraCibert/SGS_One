using Sistemas.Core.Entities.Models;
using Sistemas.Core.Interfaces.Actions;

namespace Sistemas.Core.Interfaces.Models
{
    public interface IBancoRepository : IReadRepcository<Banco, int>,ICreateRepository<Banco>,IUpdateRepository<Banco>
    {
        //Otros metodos personalizados
    }
}
