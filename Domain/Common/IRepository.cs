namespace AkataAcademy.Domain.Common
{
    public interface IRepository<T>
    where T : AggregateRoot
    {
        T GetById(Guid id);
        bool Exists(Guid id);
        void Add(T course);
        void Remove(T course);
    }
}
