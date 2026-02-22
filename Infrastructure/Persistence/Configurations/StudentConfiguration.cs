using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.Entities;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;

namespace AkataAcademy.Infrastructure.Persistence.Configurations
{
	public class StudentConfiguration : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.HasKey(s => s.Id);

			builder.OwnsOne(s => s.Name, n =>
			{
				n.Property(p => p.FirstName)
					.IsRequired()
					.HasMaxLength(50)
					.HasColumnName("FirstName");

				n.Property(p => p.LastName)
					.IsRequired()
					.HasMaxLength(50)
					.HasColumnName("LastName");
			});

			builder.Property(s => s.Email)
				.IsRequired()
				.HasMaxLength(255)
				.HasColumnName("Email")
				.HasConversion(
					e => e.Value,
					v => Email.From(v));

			builder.HasIndex(s => s.Email).IsUnique();

			builder.Property(s => s.DateOfBirth)
				.IsRequired()
				.HasColumnName("DateOfBirth")
				.HasConversion(
					d => d.Value,
					v => DateOfBirth.From(v));

			builder.Property(s => s.Status)
				.IsRequired()
				.HasConversion(
					v => v.Value,
					v => StudentStatus.From(v));
		}
	}
}
