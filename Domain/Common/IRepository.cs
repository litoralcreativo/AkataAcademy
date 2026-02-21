namespace AkataAcademy.Domain.Common
{
    public interface IRepository<T>
    where T : AggregateRoot
    {
        Task<bool> Exists(Guid id);
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T course);
        Task RemoveAsync(T course);
    }
}
