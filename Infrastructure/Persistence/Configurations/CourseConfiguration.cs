using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AkataAcademy.Domain.BoundedContexts.Catalog.Entities;

namespace AkataAcademy.Infrastructure.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.IsPublished)
                .IsRequired();

            builder.OwnsOne(c => c.Title, t =>
            {
                t.Property(p => p.Value)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            builder.OwnsOne(c => c.Description, d =>
            {
                d.Property(p => p.Value)
                    .IsRequired();
            });

            builder.HasMany(c => c.Modules)
                .WithOne()
                .HasForeignKey("CourseId");
        }
    }
}
