using AkataAcademy.Domain.BoundedContexts.Enrollment.Entities;
using AkataAcademy.Domain.BoundedContexts.Enrollment.Events;
using AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects;
using AkataAcademy.Domain.Common;
using static AkataAcademy.UnitTests.Domain.Enrollment.EnrollmentTestElements;

namespace AkataAcademy.UnitTests.Domain.Enrollment
{
	/// <summary>
	/// Test suite for EnrollmentStatus Value Object
	/// Tests validation, state transitions, and factory methods
	/// </summary>
	public class EnrollmentStatusTests
	{
		[Fact]
		public void EnrollmentStatus_FromValidEnrolled_ShouldSucceed()
		{
			var status = EnrollmentStatus.From("ENROLLED");
			Assert.NotNull(status);
			Assert.Equal("ENROLLED", status.Value);
			Assert.True(status.IsEnrolled);
		}

		[Fact]
		public void EnrollmentStatus_FromValidActive_ShouldSucceed()
		{
			var status = EnrollmentStatus.From("ACTIVE");
			Assert.NotNull(status);
			Assert.Equal("ACTIVE", status.Value);
			Assert.True(status.IsActive);
		}

		[Fact]
		public void EnrollmentStatus_FromValidCompleted_ShouldSucceed()
		{
			var status = EnrollmentStatus.From("COMPLETED");
			Assert.NotNull(status);
			Assert.Equal("COMPLETED", status.Value);
			Assert.True(status.IsCompleted);
		}

		[Fact]
		public void EnrollmentStatus_FromValidSuspended_ShouldSucceed()
		{
			var status = EnrollmentStatus.From("SUSPENDED");
			Assert.NotNull(status);
			Assert.Equal("SUSPENDED", status.Value);
			Assert.True(status.IsSuspended);
		}

		[Fact]
		public void EnrollmentStatus_FromValidDropped_ShouldSucceed()
		{
			var status = EnrollmentStatus.From("DROPPED");
			Assert.NotNull(status);
			Assert.Equal("DROPPED", status.Value);
			Assert.True(status.IsDropped);
		}

		[Fact]
		public void EnrollmentStatus_FromInvalidValue_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => EnrollmentStatus.From("INVALID_STATUS"));
		}

		[Fact]
		public void EnrollmentStatus_FromEmptyString_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => EnrollmentStatus.From(""));
		}

		[Fact]
		public void EnrollmentStatus_FromNull_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => EnrollmentStatus.From(null!));
		}

		[Fact]
		public void EnrollmentStatus_FactoryMethod_Enrolled_ShouldBeValid()
		{
			var status = EnrollmentStatus.Enrolled();
			Assert.True(status.IsEnrolled);
			Assert.True(status.CanTransitionToActive);
			Assert.False(status.CanTransitionToCompleted);
			Assert.False(status.CanTransitionToSuspended);
			Assert.True(status.CanTransitionToDropped);
		}

		[Fact]
		public void EnrollmentStatus_FactoryMethod_Active_ShouldBeValid()
		{
			var status = EnrollmentStatus.Active();
			Assert.True(status.IsActive);
			Assert.False(status.CanTransitionToActive);
			Assert.True(status.CanTransitionToCompleted);
			Assert.True(status.CanTransitionToSuspended);
			Assert.True(status.CanTransitionToDropped);
		}

		[Fact]
		public void EnrollmentStatus_FactoryMethod_Completed_ShouldBeValid()
		{
			var status = EnrollmentStatus.Completed();
			Assert.True(status.IsCompleted);
			Assert.False(status.CanTransitionToActive);
			Assert.False(status.CanTransitionToCompleted);
			Assert.False(status.CanTransitionToSuspended);
			Assert.False(status.CanTransitionToDropped);
		}

		[Fact]
		public void EnrollmentStatus_FactoryMethod_Suspended_ShouldBeValid()
		{
			var status = EnrollmentStatus.Suspended();
			Assert.True(status.IsSuspended);
			Assert.True(status.CanTransitionToActive);
			Assert.False(status.CanTransitionToCompleted);
			Assert.False(status.CanTransitionToSuspended);
			Assert.True(status.CanTransitionToDropped);
		}

		[Fact]
		public void EnrollmentStatus_FactoryMethod_Dropped_ShouldBeValid()
		{
			var status = EnrollmentStatus.Dropped();
			Assert.True(status.IsDropped);
			Assert.False(status.CanTransitionToActive);
			Assert.False(status.CanTransitionToCompleted);
			Assert.False(status.CanTransitionToSuspended);
			Assert.False(status.CanTransitionToDropped);
		}

		[Fact]
		public void EnrollmentStatus_IsCaseInsensitive()
		{
			var status1 = EnrollmentStatus.From("enrolled");
			var status2 = EnrollmentStatus.From("ENROLLED");
			var status3 = EnrollmentStatus.From("Enrolled");

			Assert.Equal(status1.Value, status2.Value);
			Assert.Equal(status2.Value, status3.Value);
		}
	}

	/// <summary>
	/// Test suite for CourseEnrollment Aggregate Root creation
	/// Tests factory methods, invariants, and initial state
	/// </summary>
	public class CourseEnrollmentCreationTests
	{
		[Fact]
		public void CreateFromEnrollment_WithValidData_ShouldSucceed()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			Assert.NotNull(enrollment);
			Assert.NotEqual(Guid.Empty, enrollment.Id);
			Assert.Equal(EnrollmentStudentId, enrollment.StudentId);
			Assert.Equal(EnrollmentCourseId, enrollment.CourseId);
			Assert.True(enrollment.Status.IsEnrolled);
			Assert.NotEqual(default(DateTime), enrollment.EnrolledOn);
			Assert.Null(enrollment.ActivatedOn);
			Assert.Null(enrollment.CompletedOn);
			Assert.Null(enrollment.SuspendedOn);
			Assert.Null(enrollment.DroppedOn);
		}

		[Fact]
		public void CreateFromEnrollment_WithNullStudentId_ShouldFail()
		{
			var result = CourseEnrollment.CreateFromEnrollment(null!, EnrollmentCourseId);

			Assert.True(result.IsFailure);
		}

		[Fact]
		public void CreateFromEnrollment_WithNullCourseId_ShouldFail()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, null!);

			Assert.True(result.IsFailure);
		}

		[Fact]
		public void CreateFromEnrollment_ShouldHaveUniqueIds()
		{
			var result1 = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			var result2 = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);

			Assert.True(result1.IsSuccess);
			Assert.True(result2.IsSuccess);
			Assert.NotEqual(result1.Value.Id, result2.Value.Id);
		}

		[Fact]
		public void CreateFromEnrollment_ShouldEmitStudentEnrolledEvent()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			var @event = enrollment.DomainEvents.OfType<StudentEnrolled>().FirstOrDefault();
			Assert.NotNull(@event);
			Assert.Equal(enrollment.Id, @event.EnrollmentId);
			Assert.Equal(EnrollmentStudentId.Value, @event.StudentId);
			Assert.Equal(EnrollmentCourseId.Value, @event.CourseId);
		}
	}

	/// <summary>
	/// Test suite for CourseEnrollment state transitions
	/// Tests Activate, CompleteLesson, Suspend, Drop and event emission
	/// </summary>
	public class CourseEnrollmentStateTransitionTests
	{
		[Fact]
		public void Activate_FromEnrolled_ShouldSucceed()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			enrollment.Activate();

			Assert.True(enrollment.Status.IsActive);
			Assert.NotNull(enrollment.ActivatedOn);
			Assert.Null(enrollment.SuspendedOn);
		}

		[Fact]
		public void Activate_FromEnrolled_ShouldEmitEnrollmentActivatedEvent()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			enrollment.Activate();

			var @event = enrollment.DomainEvents.OfType<EnrollmentActivated>().FirstOrDefault();
			Assert.NotNull(@event);
			Assert.Equal(enrollment.Id, @event.EnrollmentId);
			Assert.Equal(EnrollmentStudentId.Value, @event.StudentId);
			Assert.Equal(EnrollmentCourseId.Value, @event.CourseId);
		}

		[Fact]
		public void Activate_FromSuspended_ShouldSucceed()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();
			enrollment.Suspend("Temporary pause");

			enrollment.Activate();

			Assert.True(enrollment.Status.IsActive);
			Assert.Null(enrollment.SuspendedOn); // Cleared when resuming
		}

		[Fact]
		public void Activate_FromCompleted_ShouldThrowDomainException()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();
			for (int i = 0; i < 10; i++) enrollment.CompleteLesson();

			Assert.Throws<DomainException>(() => enrollment.Activate());
		}

		[Fact]
		public void CompleteLesson_FromActive_ShouldAdvanceProgress()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			enrollment.CompleteLesson();

			Assert.Equal(10, enrollment.Progress.Percentage.Value);
			Assert.False(enrollment.Status.IsCompleted);
		}

		[Fact]
		public void CompleteLesson_ShouldEmitLessonCompletedEvent()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			enrollment.CompleteLesson();

			var @event = enrollment.DomainEvents.OfType<LessonCompleted>().FirstOrDefault();
			Assert.NotNull(@event);
			Assert.Equal(enrollment.Id, @event.EnrollmentId);
			Assert.Equal(EnrollmentStudentId.Value, @event.StudentId);
			Assert.Equal(EnrollmentCourseId.Value, @event.CourseId);
			Assert.Equal(10, @event.CurrentPercentage);
		}

		[Fact]
		public void CompleteLesson_At100Percent_ShouldTransitionToCompleted()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			for (int i = 0; i < 10; i++)
			{
				enrollment.CompleteLesson();
			}

			Assert.True(enrollment.Status.IsCompleted);
			Assert.NotNull(enrollment.CompletedOn);
		}

		[Fact]
		public void CompleteLesson_At100Percent_ShouldEmitCourseCompletedEvent()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			for (int i = 0; i < 10; i++)
			{
				enrollment.CompleteLesson();
			}

			var @event = enrollment.DomainEvents.OfType<CourseCompleted>().FirstOrDefault();
			Assert.NotNull(@event);
			Assert.Equal(enrollment.Id, @event.EnrollmentId);
			Assert.Equal(EnrollmentStudentId.Value, @event.StudentId);
			Assert.Equal(EnrollmentCourseId.Value, @event.CourseId);
		}

		[Fact]
		public void CompleteLesson_FromEnrolled_ShouldThrowDomainException()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			Assert.Throws<DomainException>(() => enrollment.CompleteLesson());
		}

		[Fact]
		public void Suspend_FromActive_ShouldSucceed()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			enrollment.Suspend("Family emergency");

			Assert.True(enrollment.Status.IsSuspended);
			Assert.NotNull(enrollment.SuspendedOn);
		}

		[Fact]
		public void Suspend_WithNullReason_ShouldThrowDomainException()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			Assert.Throws<DomainException>(() => enrollment.Suspend(null!));
		}

		[Fact]
		public void Suspend_WithEmptyReason_ShouldThrowDomainException()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			Assert.Throws<DomainException>(() => enrollment.Suspend(""));
		}

		[Fact]
		public void Suspend_ShouldEmitEnrollmentSuspendedEvent()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			enrollment.Suspend("Temporary pause");

			var @event = enrollment.DomainEvents.OfType<EnrollmentSuspended>().FirstOrDefault();
			Assert.NotNull(@event);
			Assert.Equal(enrollment.Id, @event.EnrollmentId);
			Assert.Equal(EnrollmentStudentId.Value, @event.StudentId);
			Assert.Equal(EnrollmentCourseId.Value, @event.CourseId);
			Assert.Equal("Temporary pause", @event.Reason);
		}

		[Fact]
		public void Drop_FromEnrolled_ShouldSucceed()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			enrollment.Drop("Change of plans");

			Assert.True(enrollment.Status.IsDropped);
			Assert.NotNull(enrollment.DroppedOn);
		}

		[Fact]
		public void Drop_WithNullReason_ShouldThrowDomainException()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			Assert.Throws<DomainException>(() => enrollment.Drop(null!));
		}

		[Fact]
		public void Drop_WithEmptyReason_ShouldThrowDomainException()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			Assert.Throws<DomainException>(() => enrollment.Drop(""));
		}

		[Fact]
		public void Drop_ShouldEmitEnrollmentDroppedEvent()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			enrollment.Drop("Not interested");

			var @event = enrollment.DomainEvents.OfType<EnrollmentDropped>().FirstOrDefault();
			Assert.NotNull(@event);
			Assert.Equal(enrollment.Id, @event.EnrollmentId);
			Assert.Equal(EnrollmentStudentId.Value, @event.StudentId);
			Assert.Equal(EnrollmentCourseId.Value, @event.CourseId);
			Assert.Equal("Not interested", @event.Reason);
		}

		[Fact]
		public void Drop_FromCompleted_ShouldThrowDomainException()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();
			for (int i = 0; i < 10; i++) enrollment.CompleteLesson();

			Assert.Throws<DomainException>(() => enrollment.Drop("Too late"));
		}
	}

	/// <summary>
	/// Test suite for CourseEnrollment helper methods
	/// Tests IsOngoing, GetDaysSinceEnrollment, GetDaysSinceActivation
	/// </summary>
	public class CourseEnrollmentHelperMethodTests
	{
		[Fact]
		public void IsOngoing_WhenEnrolled_ShouldReturnTrue()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			Assert.True(enrollment.IsOngoing);
		}

		[Fact]
		public void IsOngoing_WhenActive_ShouldReturnTrue()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			Assert.True(enrollment.IsOngoing);
		}

		[Fact]
		public void IsOngoing_WhenSuspended_ShouldReturnTrue()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();
			enrollment.Suspend("Pause");

			Assert.True(enrollment.IsOngoing);
		}

		[Fact]
		public void IsOngoing_WhenCompleted_ShouldReturnFalse()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();
			for (int i = 0; i < 10; i++) enrollment.CompleteLesson();

			Assert.False(enrollment.IsOngoing);
		}

		[Fact]
		public void IsOngoing_WhenDropped_ShouldReturnFalse()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			enrollment.Drop("Change of plans");

			Assert.False(enrollment.IsOngoing);
		}

		[Fact]
		public void GetDaysSinceEnrollment_ShouldReturnNonNegativeNumber()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			var days = enrollment.GetDaysSinceEnrollment();

			Assert.True(days >= 0);
		}

		[Fact]
		public void GetDaysSinceActivation_WhenNotActivated_ShouldReturnNull()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			var days = enrollment.GetDaysSinceActivation();

			Assert.Null(days);
		}

		[Fact]
		public void GetDaysSinceActivation_WhenActivated_ShouldReturnNonNegativeNumber()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			var days = enrollment.GetDaysSinceActivation();

			Assert.NotNull(days);
			Assert.True(days >= 0);
		}
	}

	/// <summary>
	/// Test suite for Progress entity
	/// Tests percentage advancement and completion tracking
	/// </summary>
	public class ProgressTests
	{
		[Fact]
		public void Progress_Create_ShouldStartAt0Percent()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			Assert.Equal(0, enrollment.Progress.Percentage.Value);
			Assert.False(enrollment.Progress.IsFullyCompleted);
		}

		[Fact]
		public void Progress_Advance_ShouldIncrementBy10Percent()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			enrollment.CompleteLesson();

			Assert.Equal(10, enrollment.Progress.Percentage.Value);
		}

		[Fact]
		public void Progress_Advance_ShouldCapsAt100Percent()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			// Complete all 10 lessons to reach 100%
			for (int i = 0; i < 10; i++)
			{
				enrollment.CompleteLesson();
			}

			Assert.Equal(100, enrollment.Progress.Percentage.Value);
		}

		[Fact]
		public void Progress_GetRemainingLessons_ShouldCalculateCorrectly()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			var remaining = enrollment.Progress.GetRemainingLessons();

			Assert.Equal(10, remaining); // 100 / 10 = 10 lessons
		}

		[Fact]
		public void Progress_GetRemainingLessons_AfterSomeLessons_ShouldCalculateCorrectly()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();
			enrollment.CompleteLesson();
			enrollment.CompleteLesson();
			enrollment.CompleteLesson();

			var remaining = enrollment.Progress.GetRemainingLessons();

			Assert.Equal(7, remaining); // (100 - 30) / 10 = 7 lessons
		}

		[Fact]
		public void Progress_IsFullyCompleted_At100Percent()
		{
			var result = CourseEnrollment.CreateFromEnrollment(EnrollmentStudentId, EnrollmentCourseId);
			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			for (int i = 0; i < 10; i++)
			{
				enrollment.CompleteLesson();
			}

			Assert.True(enrollment.Progress.IsFullyCompleted);
		}
	}
}
