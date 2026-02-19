namespace AkataAcademy.Application.Common
{
	public interface IIntegrationEventHandler<TEvent>
	{
		void Handle(TEvent @event);
	}
}