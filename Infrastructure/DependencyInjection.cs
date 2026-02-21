using AkataAcademy.Infrastructure.Persistence;
using AkataAcademy.Infrastructure.Messaging;
using AkataAcademy.Application.Common;
using AkataAcademy.Application.Catalog.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AkataAcademy.Domain.BoundedContexts.Catalog.Repositories;
using AkataAcademy.Domain.BoundedContexts.Certification.Repositories;
using AkataAcademy.Application.Certification.Queries;

namespace AkataAcademy.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Event Dispatchers and Publishers

            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddSingleton<IEventBus, InMemoryEventBus>();

            #endregion

            #region Database Configuration    

            services.AddDbContext<ApplicationDbContext>(
                options => options
                    .UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            #endregion

            #region Repositories

            services.AddScoped<ICourseReadRepository, CourseReadRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<ICertificateReadRepository, CertificateReadRepository>();

            #endregion

            return services;
        }
    }
}