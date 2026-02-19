namespace AkataAcademy.Application.Common
{
	public interface IEventBus
	{
		void Publish<TEvent>(TEvent @event)
			where TEvent : class;
	}
}