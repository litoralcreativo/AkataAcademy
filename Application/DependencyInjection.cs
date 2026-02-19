using AkataAcademy.Application.Common;
using AkataAcademy.Application.Dispatchers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AkataAcademy.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registro automático de todos los ICommandHandler y IQueryHandler en este ensamblado
            var assembly = Assembly.GetExecutingAssembly();
            var handlerTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                .Where(x => x.Interface.IsGenericType &&
                            (x.Interface.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                             x.Interface.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                             x.Interface.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                .ToList();

            foreach (var handler in handlerTypes)
            {
                services.AddTransient(handler.Interface, handler.Type);
            }

            // Registro de los dispatchers CQRS
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();

            return services;
        }
    }
}