using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Catalog.Entities;
using AkataAcademy.Domain.BoundedContexts.Certification.Entities;
using AkataAcademy.Domain.BoundedContexts.Enrollment.Entities;
using AkataAcademy.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace AkataAcademy.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {

        private readonly IDomainEventDispatcher _dispatcher;

        public ApplicationDbContext(DbContextOptions options, IDomainEventDispatcher dispatcher)
            : base(options)
        {
            _dispatcher = dispatcher;
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseModule> CourseModules { get; set; }
        public DbSet<CourseEnrollment> Enrollments { get; set; }
        public DbSet<Certificate> Certificates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var entitiesWithEvents = ChangeTracker.Entries<Entity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToList();

            var domainEvents = entitiesWithEvents.SelectMany(e => e.DomainEvents).ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _dispatcher.Dispatch(domainEvent, cancellationToken);
            }

            foreach (var entity in entitiesWithEvents)
            {
                entity.ClearDomainEvents();
            }

            return result;
        }
    }
}
