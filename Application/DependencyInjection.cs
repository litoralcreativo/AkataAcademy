using AkataAcademy.Application.Common;
using AkataAcademy.Application.Dispatchers;
using AkataAcademy.Domain.Certification.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AkataAcademy.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register all handlers (commands, queries, domain events, integration events)
            var assembly = Assembly.GetExecutingAssembly();
            var handlerTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                .Where(x => x.Interface.IsGenericType &&
                    (x.Interface.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                     x.Interface.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                     x.Interface.GetGenericTypeDefinition() == typeof(IQueryHandler<,>) ||
                     x.Interface.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>) ||
                    x.Interface.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>)))
                .ToList();

            foreach (var handler in handlerTypes)
            {
                services.AddTransient(handler.Interface, handler.Type);
            }

            // Register CQRS dispatchers
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();

            // Register domain services
            services.AddTransient<CertificateDomainService>();

            return services;
        }
    }
}