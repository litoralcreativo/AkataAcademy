using AkataAcademy.Application.Common;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Infrastructure.Messaging
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Dispatch(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IDomainEventHandler<>)
                .MakeGenericType(domainEvent.GetType());

            var handlers = _serviceProvider
                .GetService(typeof(IEnumerable<>).MakeGenericType(handlerType)) as System.Collections.IEnumerable;

            if (handlers == null)
                return;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[DomainEventDispatcher] Dispatching event: {domainEvent.GetType().Name}");
            Console.ResetColor();

            foreach (var handler in handlers)
            {
                var method = handlerType.GetMethod("Handle");
                if (method == null)
                    continue;

                method.Invoke(handler, new object[] { domainEvent });
            }
        }
    }
}
