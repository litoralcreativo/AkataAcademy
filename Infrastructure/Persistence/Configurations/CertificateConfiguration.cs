using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AkataAcademy.Domain.BoundedContexts.Certification.Entities;

namespace AkataAcademy.Infrastructure.Persistence.Configurations
{
	public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
	{
		public void Configure(EntityTypeBuilder<Certificate> builder)
		{
			builder.HasKey(c => c.Id);

			builder.OwnsOne(c => c.StudentId, s =>
			{
				s.Property(p => p.Value)
					.IsRequired();
			});

			builder.OwnsOne(c => c.CourseId, co =>
			{
				co.Property(p => p.Value)
					.IsRequired();
			});

			builder.OwnsOne(c => c.IssuedOn, i =>
			{
				i.Property(p => p.Value)
					.IsRequired();
			});

			builder.OwnsOne(c => c.ExpiresOn, e =>
			{
				e.Property(p => p.Value)
					.IsRequired();
			});
		}
	}
}
