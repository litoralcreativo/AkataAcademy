using AkataAcademy.Application.Common;

namespace AkataAcademy.Infrastructure.Messaging
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : class
        {
            var handlerType = typeof(IIntegrationEventHandler<>)
                .MakeGenericType(typeof(TEvent));

            var handlers = _serviceProvider
                .GetService(typeof(IEnumerable<>).MakeGenericType(handlerType)) as System.Collections.IEnumerable;

            if (handlers == null)
                return;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[InMemoryEventBus] Publishing event: {typeof(TEvent).Name}");
            Console.ResetColor();

            foreach (var handler in handlers)
            {
                var method = handlerType.GetMethod("Handle");
                if (method == null)
                    continue;

                method.Invoke(handler, new object[] { @event });
            }
        }
    }
}
