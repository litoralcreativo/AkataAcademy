using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;
using AkataAcademy.Domain.BoundedContexts.Enrollment.Entities;
using AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AkataAcademy.Infrastructure.Persistence.Configurations
{
	/// <summary>
	/// EF Core configuration for CourseEnrollment Aggregate Root
	/// Configures the enrollment lifecycle with:
	/// - Multiple status values
	/// - Configurable progress increments
	/// - Owned Progress entity with ProgressIncrement value object
	/// </summary>
	public class CourseEnrollmentConfiguration : IEntityTypeConfiguration<CourseEnrollment>
	{
		public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
		{
			builder.HasKey(e => e.Id);

			// Configure StudentId (Value Object) - using Value Converter
			builder.Property(e => e.StudentId)
				.HasConversion(
					v => v.Value,
					v => new StudentId(v))
				.IsRequired();

			// Configure CourseId (Value Object) - using Value Converter
			builder.Property(e => e.CourseId)
				.HasConversion(
					v => v.Value,
					v => new CourseId(v))
				.IsRequired();

			// Configure EnrollmentStatus (Value Object)
			builder.OwnsOne(e => e.Status, st =>
			{
				st.Property(p => p.Value)
					.HasColumnName("Status")
					.IsRequired();
			});

			// Configure ProgressIncrement (Value Object)
			// Stored as a single integer percentage value
			builder.OwnsOne(e => e.ProgressIncrement, pi =>
			{
				pi.Property(p => p.Value)
					.HasColumnName("ProgressIncrement")
					.IsRequired();
			});

			// Configure Progress (Owned Entity)
			// Contains CompletionPercentage (VO) and LessonsCompleted (int)
			builder.OwnsOne(e => e.Progress, p =>
			{
				p.WithOwner();

				// Progress ID
				p.Property(pr => pr.Id)
					.HasColumnName("ProgressId");

				// CompletionPercentage (Value Object)
				p.OwnsOne(pr => pr.Percentage, cp =>
				{
					cp.Property(v => v.Value)
						.HasColumnName("ProgressPercentage")
						.IsRequired();
				});

				// ProgressIncrement stored in Progress for tracking
				p.OwnsOne(pr => pr.Increment, pi =>
				{
					pi.Property(v => v.Value)
						.HasColumnName("ProgressIncrementValue")
						.IsRequired();
				});

				// LessonsCompleted counter
				p.Property(pr => pr.LessonsCompleted)
					.HasColumnName("LessonsCompleted")
					.IsRequired();
			});

			// Configure timestamp properties
			builder.Property(e => e.EnrolledOn)
				.IsRequired();

			builder.Property(e => e.ActivatedOn)
				.IsRequired(false);

			builder.Property(e => e.CompletedOn)
				.IsRequired(false);

			builder.Property(e => e.SuspendedOn)
				.IsRequired(false);

			builder.Property(e => e.DroppedOn)
				.IsRequired(false);

			// Indexes for common queries
			builder.HasIndex(e => e.StudentId);
			builder.HasIndex(e => e.CourseId);
			builder.HasIndex(e => new { e.StudentId, e.CourseId })
				.IsUnique();

			// Table name
			builder.ToTable("CourseEnrollments");
		}
	}
}
