using AkataAcademy.Infrastructure.Persistence;
using AkataAcademy.Infrastructure.Messaging;
using AkataAcademy.Application.Common;
using AkataAcademy.Application.Catalog.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AkataAcademy.Domain.BoundedContexts.Catalog.Repositories;

namespace AkataAcademy.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			#region DomainEventDispatcher

			services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();

			#endregion


			#region Database Configuration	

			services.AddDbContext<ApplicationDbContext>(
				options => options.UseInMemoryDatabase("AkataAcademyDb"));

			services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

			#endregion

			#region Repositories

			services.AddScoped<ICourseReadRepository, CourseReadRepository>();
			services.AddScoped<ICourseRepository, CourseRepository>();

			#endregion

			return services;

		}
	}
}