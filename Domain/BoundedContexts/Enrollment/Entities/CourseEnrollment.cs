using AkataAcademy.Domain.BoundedContexts.Enrollment.Events;
using AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.Entities
{
	/// <summary>
	/// CourseEnrollment Aggregate Root
	/// 
	/// Represents a student's enrollment in a course with the following lifecycle:
	/// 1. Enrolled (initial state) - student just enrolled
	/// 2. Active - student started the course
	/// 3. Completed - student finished the course (100% progress)
	/// 
	/// Alternative paths:
	/// - Suspended - student paused the course (from Active)
	/// - Dropped - student dropped the course (from any state except Completed)
	/// 
	/// State transitions are enforced by EnrollmentStatus Value Object
	/// </summary>
	public class CourseEnrollment : AggregateRoot
	{
		public StudentId StudentId { get; private set; } = null!;
		public CourseId CourseId { get; private set; } = null!;
		public EnrollmentStatus Status { get; private set; } = null!;
		public Progress Progress { get; private set; } = null!;
		public ProgressIncrement ProgressIncrement { get; private set; } = null!;
		public DateTime EnrolledOn { get; private set; }
		public DateTime? ActivatedOn { get; private set; }
		public DateTime? CompletedOn { get; private set; }
		public DateTime? SuspendedOn { get; private set; }
		public DateTime? DroppedOn { get; private set; }

		protected CourseEnrollment() { } // EF

		private CourseEnrollment(StudentId studentId, CourseId courseId, ProgressIncrement? progressIncrement = null)
		{
			Id = Guid.NewGuid();
			StudentId = studentId;
			CourseId = courseId;
			Status = EnrollmentStatus.Enrolled();
			ProgressIncrement = progressIncrement ?? ProgressIncrement.Standard();
			Progress = Progress.CreateWithIncrement(ProgressIncrement);
			EnrolledOn = DateTime.UtcNow;
			ActivatedOn = null;
			CompletedOn = null;
			SuspendedOn = null;
			DroppedOn = null;

			AddDomainEvent(new StudentEnrolled(Id, StudentId.Value, CourseId.Value));
		}

		/// <summary>
		/// Creates a new course enrollment with validation using Result pattern
		/// </summary>
		public static Result<CourseEnrollment> CreateFromEnrollment(StudentId studentId, CourseId courseId)
		{
			try
			{
				if (studentId == null)
					return Result.Failure<CourseEnrollment>(Error.Validation("StudentId", "StudentId is required"));

				if (courseId == null)
					return Result.Failure<CourseEnrollment>(Error.Validation("CourseId", "CourseId is required"));

				var enrollment = new CourseEnrollment(studentId, courseId);
				return Result.Success(enrollment);
			}
			catch (Exception ex)
			{
				return Result.Failure<CourseEnrollment>(Error.Failure(ErrorCodes.Enrollment.Creation, ex.Message));
			}
		}

		/// <summary>
		/// Creates a new course enrollment with custom progress increment
		/// </summary>
		public static Result<CourseEnrollment> CreateFromEnrollmentWithProgress(
			StudentId studentId,
			CourseId courseId,
			ProgressIncrement progressIncrement)
		{
			try
			{
				if (studentId == null)
					return Result.Failure<CourseEnrollment>(Error.Validation("StudentId", "StudentId is required"));

				if (courseId == null)
					return Result.Failure<CourseEnrollment>(Error.Validation("CourseId", "CourseId is required"));

				if (progressIncrement == null)
					return Result.Failure<CourseEnrollment>(Error.Validation("ProgressIncrement", "ProgressIncrement is required"));

				var enrollment = new CourseEnrollment(studentId, courseId, progressIncrement);
				return Result.Success(enrollment);
			}
			catch (Exception ex)
			{
				return Result.Failure<CourseEnrollment>(Error.Failure(ErrorCodes.Enrollment.Creation, ex.Message));
			}
		}

		/// <summary>
		/// Transitions enrollment from Enrolled to Active state
		/// Rule: Only from Enrolled or Suspended state
		/// </summary>
		public void Activate()
		{
			if (!Status.CanTransitionToActive)
				throw new DomainException($"Cannot transition from {Status.Value} to Active state.");

			Status = EnrollmentStatus.Active();
			ActivatedOn = DateTime.UtcNow;
			SuspendedOn = null; // Clear suspended timestamp if resuming

			AddDomainEvent(new EnrollmentActivated(Id, StudentId.Value, CourseId.Value));
		}

		/// <summary>
		/// Completes a lesson by advancing progress
		/// Rule: Only from Active state
		/// Automatically transitions to Completed when progress reaches 100%
		/// </summary>
		public void CompleteLesson()
		{
			if (!Status.IsActive)
				throw new DomainException($"Cannot complete lesson from {Status.Value} state. Must be Active.");

			Progress.Advance();

			AddDomainEvent(new LessonCompleted(Id, StudentId.Value, CourseId.Value, Progress.Percentage.Value));

			if (Progress.Percentage.Value == 100)
			{
				Status = EnrollmentStatus.Completed();
				CompletedOn = DateTime.UtcNow;
				AddDomainEvent(new CourseCompleted(Id, StudentId.Value, CourseId.Value));
			}
		}

		/// <summary>
		/// Suspends the enrollment temporarily
		/// Rule: Only from Active state
		/// Can be resumed by calling Activate()
		/// </summary>
		public void Suspend(string reason)
		{
			if (!Status.CanTransitionToSuspended)
				throw new DomainException($"Cannot suspend enrollment from {Status.Value} state. Must be Active.");

			if (string.IsNullOrWhiteSpace(reason))
				throw new DomainException("Suspension reason is required.");

			Status = EnrollmentStatus.Suspended();
			SuspendedOn = DateTime.UtcNow;

			AddDomainEvent(new EnrollmentSuspended(Id, StudentId.Value, CourseId.Value, reason));
		}

		/// <summary>
		/// Drops the enrollment permanently
		/// Rule: Cannot drop from Completed or already Dropped state
		/// </summary>
		public void Drop(string reason)
		{
			if (!Status.CanTransitionToDropped)
				throw new DomainException($"Cannot drop enrollment from {Status.Value} state.");

			if (string.IsNullOrWhiteSpace(reason))
				throw new DomainException("Drop reason is required.");

			Status = EnrollmentStatus.Dropped();
			DroppedOn = DateTime.UtcNow;

			AddDomainEvent(new EnrollmentDropped(Id, StudentId.Value, CourseId.Value, reason));
		}

		/// <summary>
		/// Determines if the enrollment is still active and ongoing
		/// </summary>
		public bool IsOngoing => !Status.IsCompleted && !Status.IsDropped;

		/// <summary>
		/// Calculates the number of days since enrollment started
		/// </summary>
		public int GetDaysSinceEnrollment()
		{
			return (int)(DateTime.UtcNow - EnrolledOn).TotalDays;
		}

		/// <summary>
		/// Calculates the number of days since the enrollment was activated
		/// Returns null if not activated yet
		/// </summary>
		public int? GetDaysSinceActivation()
		{
			return ActivatedOn.HasValue ? (int)(DateTime.UtcNow - ActivatedOn.Value).TotalDays : null;
		}
	}
}