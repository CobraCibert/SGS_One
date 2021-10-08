using Sistemas.Core.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistemas.Core.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        IBancoRepository BancoRepository { get; }
    }
}
