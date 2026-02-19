using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Common
{
	public interface IDomainEventDispatcher
	{
		void Dispatch(IDomainEvent domainEvent);
	}
}