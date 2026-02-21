namespace AkataAcademy.Application.Common
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent @event)
            where TEvent : class;
    }
}