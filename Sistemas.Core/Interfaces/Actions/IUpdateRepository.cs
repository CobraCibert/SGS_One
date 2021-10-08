namespace Sistemas.Core.Interfaces.Actions
{
    public interface IUpdateRepository<T> where T : class
    {
        void Update(T t);
    }
}
