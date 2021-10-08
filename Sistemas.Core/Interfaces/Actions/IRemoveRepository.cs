namespace Sistemas.Core.Interfaces.Actions
{
    public interface IRemoveRepository<T>
    {
        void Remove(T id);
    }
}