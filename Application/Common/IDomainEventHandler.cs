using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Common
{
	public interface IDomainEventHandler<TEvent>
	where TEvent : IDomainEvent
	{
		void Handle(TEvent domainEvent);
	}
}