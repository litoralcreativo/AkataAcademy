using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AkataAcademy.Domain.BoundedContexts.Catalog.Entities;

namespace AkataAcademy.Infrastructure.Persistence.Configurations
{
    public class CourseModuleConfiguration : IEntityTypeConfiguration<CourseModule>
    {
        public void Configure(EntityTypeBuilder<CourseModule> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.CourseId).IsRequired();

            builder.OwnsOne(m => m.Title, t =>
            {
                t.Property(p => p.Value)
                    .IsRequired();
            });

            builder.OwnsOne(m => m.Duration, d =>
            {
                d.Property(p => p.Minutes)
                    .IsRequired();
            });
        }
    }
}
