using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.Events
{
	/// <summary>
	/// Emitted when a student enrolls in a course
	/// </summary>
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

	/// <summary>
	/// Emitted when an enrollment transitions to Active state
	/// </summary>
	public record EnrollmentActivated(
		Guid EnrollmentId,
		Guid StudentId,
		Guid CourseId,
		DateTime OccurredOn
	) : IDomainEvent
	{
		public EnrollmentActivated(Guid enrollmentId, Guid studentId, Guid courseId)
			: this(enrollmentId, studentId, courseId, DateTime.UtcNow) { }
	}

	/// <summary>
	/// Emitted when a lesson is completed and progress is advanced
	/// </summary>
	public record LessonCompleted(
		Guid EnrollmentId,
		Guid StudentId,
		Guid CourseId,
		int CurrentPercentage,
		DateTime OccurredOn
	) : IDomainEvent
	{
		public LessonCompleted(Guid enrollmentId, Guid studentId, Guid courseId, int percentage)
			: this(enrollmentId, studentId, courseId, percentage, DateTime.UtcNow) { }
	}

	/// <summary>
	/// Emitted when a course is completed (100% progress)
	/// </summary>
	public record CourseCompleted(
		Guid EnrollmentId,
		Guid StudentId,
		Guid CourseId,
		DateTime OccurredOn
	) : IDomainEvent
	{
		public CourseCompleted(Guid enrollmentId, Guid studentId, Guid courseId)
			: this(enrollmentId, studentId, courseId, DateTime.UtcNow) { }
	}

	/// <summary>
	/// Emitted when an enrollment is suspended
	/// </summary>
	public record EnrollmentSuspended(
		Guid EnrollmentId,
		Guid StudentId,
		Guid CourseId,
		string Reason,
		DateTime OccurredOn
	) : IDomainEvent
	{
		public EnrollmentSuspended(Guid enrollmentId, Guid studentId, Guid courseId, string reason)
			: this(enrollmentId, studentId, courseId, reason, DateTime.UtcNow) { }
	}

	/// <summary>
	/// Emitted when an enrollment is dropped
	/// </summary>
	public record EnrollmentDropped(
		Guid EnrollmentId,
		Guid StudentId,
		Guid CourseId,
		string Reason,
		DateTime OccurredOn
	) : IDomainEvent
	{
		public EnrollmentDropped(Guid enrollmentId, Guid studentId, Guid courseId, string reason)
			: this(enrollmentId, studentId, courseId, reason, DateTime.UtcNow) { }
	}
}