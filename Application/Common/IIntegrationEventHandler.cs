namespace AkataAcademy.Application.Common
{
    public interface IIntegrationEventHandler<TEvent> 
    where TEvent : IIntegrationEvent
    {
        Task Handle(TEvent @event);
    }
}