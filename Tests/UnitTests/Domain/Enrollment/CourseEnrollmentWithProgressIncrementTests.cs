using AkataAcademy.Domain.BoundedContexts.Enrollment.Entities;
using AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects;
using AkataAcademy.Domain.Common;
using Xunit;
using static AkataAcademy.UnitTests.Domain.Enrollment.EnrollmentTestElements;

namespace AkataAcademy.UnitTests.Domain.Enrollment
{
	/// <summary>
	/// Test suite for CourseEnrollment with configurable ProgressIncrement
	/// Tests different progression speeds: Fast (5%), Standard (10%), Slow (20%), and Custom
	/// </summary>
	public class CourseEnrollmentWithProgressIncrementTests
	{
		[Fact]
		public void CreateFromEnrollmentWithProgress_WithStandardIncrement_ShouldSucceed()
		{
			var increment = ProgressIncrement.Standard();
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			Assert.Equal(increment.Value, enrollment.ProgressIncrement.Value);
			Assert.Equal(0, enrollment.Progress.Percentage.Value);
		}

		[Fact]
		public void CreateFromEnrollmentWithProgress_WithFastIncrement_ShouldSucceed()
		{
			var increment = ProgressIncrement.Fast();
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			Assert.Equal(5, enrollment.ProgressIncrement.Value);
		}

		[Fact]
		public void CreateFromEnrollmentWithProgress_WithSlowIncrement_ShouldSucceed()
		{
			var increment = ProgressIncrement.Slow();
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			Assert.Equal(20, enrollment.ProgressIncrement.Value);
		}

		[Fact]
		public void CreateFromEnrollmentWithProgress_WithCustomIncrement_ShouldSucceed()
		{
			var increment = ProgressIncrement.Custom(15);
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			Assert.Equal(15, enrollment.ProgressIncrement.Value);
		}

		[Fact]
		public void CreateFromEnrollmentWithProgress_WithNullStudentId_ShouldFail()
		{
			var increment = ProgressIncrement.Standard();
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				null!,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsFailure);
		}

		[Fact]
		public void CreateFromEnrollmentWithProgress_WithNullCourseId_ShouldFail()
		{
			var increment = ProgressIncrement.Standard();
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				null!,
				increment);

			Assert.True(result.IsFailure);
		}

		[Fact]
		public void CreateFromEnrollmentWithProgress_WithNullIncrement_ShouldFail()
		{
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				null!);

			Assert.True(result.IsFailure);
		}

		[Fact]
		public void FastTrackProgression_5PercentPerLesson_ShouldCompleteIn20Lessons()
		{
			var increment = ProgressIncrement.Fast();
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			// Complete 20 lessons at 5% each = 100%
			for (int i = 0; i < 20; i++)
			{
				enrollment.CompleteLesson();
				if (i < 19)
					Assert.False(enrollment.Status.IsCompleted);
			}

			Assert.True(enrollment.Status.IsCompleted);
			Assert.Equal(100, enrollment.Progress.Percentage.Value);
		}

		[Fact]
		public void FastTrackProgression_ProgressPercentage_ShouldAdvanceBy5()
		{
			var increment = ProgressIncrement.Fast();
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			enrollment.CompleteLesson();

			Assert.Equal(5, enrollment.Progress.Percentage.Value);
		}

		[Fact]
		public void SlowTrackProgression_20PercentPerLesson_ShouldCompleteIn5Lessons()
		{
			var increment = ProgressIncrement.Slow();
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			// Complete 5 lessons at 20% each = 100%
			for (int i = 0; i < 5; i++)
			{
				enrollment.CompleteLesson();
				if (i < 4)
					Assert.False(enrollment.Status.IsCompleted);
			}

			Assert.True(enrollment.Status.IsCompleted);
			Assert.Equal(100, enrollment.Progress.Percentage.Value);
		}

		[Fact]
		public void SlowTrackProgression_ProgressPercentage_ShouldAdvanceBy20()
		{
			var increment = ProgressIncrement.Slow();
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			enrollment.CompleteLesson();

			Assert.Equal(20, enrollment.Progress.Percentage.Value);
		}

		[Fact]
		public void CustomTrackProgression_15PercentPerLesson_ShouldCompleteInCeilingOf7Lessons()
		{
			var increment = ProgressIncrement.Custom(15);
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			// Complete 7 lessons: 6 * 15% = 90%, then 7th = 100% (capped)
			for (int i = 0; i < 7; i++)
			{
				enrollment.CompleteLesson();
				if (i < 6)
					Assert.False(enrollment.Status.IsCompleted);
			}

			Assert.True(enrollment.Status.IsCompleted);
			Assert.Equal(100, enrollment.Progress.Percentage.Value);
		}

		[Fact]
		public void CustomTrackProgression_12PercentPerLesson_ShouldCompleteCorrectly()
		{
			var increment = ProgressIncrement.Custom(12);
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			enrollment.CompleteLesson();
			Assert.Equal(12, enrollment.Progress.Percentage.Value);

			enrollment.CompleteLesson();
			Assert.Equal(24, enrollment.Progress.Percentage.Value);

			enrollment.CompleteLesson();
			Assert.Equal(36, enrollment.Progress.Percentage.Value);
		}

		[Fact]
		public void ProgressIncrement_IsStoredInEnrollment()
		{
			var increment = ProgressIncrement.Custom(17);
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;

			Assert.NotNull(enrollment.ProgressIncrement);
			Assert.Equal(17, enrollment.ProgressIncrement.Value);
		}

		[Fact]
		public void ProgressRemainingLessons_WithFastTrack_ShouldCalculateCorrectly()
		{
			var increment = ProgressIncrement.Fast();
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			// At 0%, should need 20 lessons (100 / 5)
			var initialRemaining = enrollment.Progress.GetRemainingLessons();
			Assert.Equal(20, initialRemaining);

			// Complete 5 lessons (25%)
			for (int i = 0; i < 5; i++)
				enrollment.CompleteLesson();

			// At 25%, should need 15 lessons (75 / 5)
			var afterFiveRemaining = enrollment.Progress.GetRemainingLessons();
			Assert.Equal(15, afterFiveRemaining);
		}

		[Fact]
		public void ProgressRemainingLessons_WithSlowTrack_ShouldCalculateCorrectly()
		{
			var increment = ProgressIncrement.Slow();
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();

			// At 0%, should need 5 lessons (100 / 20)
			var initialRemaining = enrollment.Progress.GetRemainingLessons();
			Assert.Equal(5, initialRemaining);

			// Complete 2 lessons (40%)
			enrollment.CompleteLesson();
			enrollment.CompleteLesson();

			// At 40%, should need 3 lessons (60 / 20)
			var afterTwoRemaining = enrollment.Progress.GetRemainingLessons();
			Assert.Equal(3, afterTwoRemaining);
		}

		[Fact]
		public void DifferentEnrollments_CanHaveDifferentProgressIncrements()
		{
			var fastResult = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				ProgressIncrement.Fast());

			var slowResult = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				ProgressIncrement.Slow());

			Assert.True(fastResult.IsSuccess);
			Assert.True(slowResult.IsSuccess);

			var fastEnrollment = fastResult.Value;
			var slowEnrollment = slowResult.Value;

			Assert.Equal(5, fastEnrollment.ProgressIncrement.Value);
			Assert.Equal(20, slowEnrollment.ProgressIncrement.Value);
			Assert.NotEqual(fastEnrollment.Id, slowEnrollment.Id);
		}

		[Fact]
		public void FastTrackEnrollment_ShouldCompleteQuicker_InTermsOfLessons()
		{
			var fastResult = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				ProgressIncrement.Fast());

			var standardResult = CourseEnrollment.CreateFromEnrollment(
				EnrollmentStudentId,
				EnrollmentCourseId);

			Assert.True(fastResult.IsSuccess);
			Assert.True(standardResult.IsSuccess);

			var fastEnrollment = fastResult.Value;
			var standardEnrollment = standardResult.Value;

			var fastLessonsNeeded = fastEnrollment.ProgressIncrement.GetLessonsNeeded();
			var standardLessonsNeeded = standardEnrollment.ProgressIncrement.GetLessonsNeeded();

			// Fast track needs MORE lessons but completes FASTER in terms of time
			Assert.Equal(20, fastLessonsNeeded);
			Assert.Equal(10, standardLessonsNeeded);
		}

		[Fact]
		public void CustomIncrement_1Percent_RequiresMany100Lessons()
		{
			var increment = ProgressIncrement.Custom(1);
			var lessonsNeeded = increment.GetLessonsNeeded();

			Assert.Equal(100, lessonsNeeded);
		}

		[Fact]
		public void CustomIncrement_50Percent_RequiresOnly2Lessons()
		{
			var increment = ProgressIncrement.Custom(50);
			var lessonsNeeded = increment.GetLessonsNeeded();

			Assert.Equal(2, lessonsNeeded);
		}

		[Fact]
		public void EnrollmentWithProgressIncrement_CanSuspendAndResume()
		{
			var increment = ProgressIncrement.Fast();
			var result = CourseEnrollment.CreateFromEnrollmentWithProgress(
				EnrollmentStudentId,
				EnrollmentCourseId,
				increment);

			Assert.True(result.IsSuccess);
			var enrollment = result.Value;
			enrollment.Activate();
			enrollment.CompleteLesson();
			enrollment.CompleteLesson();

			Assert.Equal(10, enrollment.Progress.Percentage.Value);

			enrollment.Suspend("Need a break");
			Assert.True(enrollment.Status.IsSuspended);
			Assert.Equal(10, enrollment.Progress.Percentage.Value); // Progress persisted

			enrollment.Activate();
			Assert.True(enrollment.Status.IsActive);
			Assert.Equal(10, enrollment.Progress.Percentage.Value);

			enrollment.CompleteLesson();
			Assert.Equal(15, enrollment.Progress.Percentage.Value);
		}
	}
}
