using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Common
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }
}