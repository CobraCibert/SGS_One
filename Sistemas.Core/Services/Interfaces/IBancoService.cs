using Sistemas.Core.Entities.Models;
using System.Collections.Generic;

namespace Sistemas.Core.Services.Interfaces
{
    public interface IBancoService
    {
        IEnumerable<Banco> GetAll();
        Banco Get(int id);
        void Create(Banco model);
        void Update(Banco model);
    }
}
