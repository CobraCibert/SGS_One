using System.Collections.Generic;

namespace Sistemas.Core.Interfaces.Actions
{
    public interface IReadRepcository<T, Y> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Y id);
    }
}
