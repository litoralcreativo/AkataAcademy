using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.Events
{
	public record CourseCompleted(Guid EnrollmentId, DateTime OccurredOn) : IDomainEvent
	{
		public CourseCompleted(Guid enrollmentId) : this(enrollmentId, DateTime.UtcNow) { }
	}

	public record LessonCompleted(Guid EnrollmentId, int CurrentPercentage, DateTime OccurredOn) : IDomainEvent
	{
		public LessonCompleted(Guid enrollmentId, int percentage) : this(enrollmentId, percentage, DateTime.UtcNow) { }
	}
	public record StudentEnrolled(
		Guid EnrollmentId,
		Guid StudentId,
		Guid CourseId,
		DateTime OccurredOn
	) : IDomainEvent
	{
		public StudentEnrolled(Guid enrollmentId, Guid studentId, Guid courseId)
			: this(enrollmentId, studentId, courseId, DateTime.UtcNow) { }
	}
}