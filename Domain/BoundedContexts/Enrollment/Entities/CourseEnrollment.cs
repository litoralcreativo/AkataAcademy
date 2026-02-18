using Domain.BoundedContexts.Enrollment.Events;
using Domain.BoundedContexts.Enrollment.ValueObjects;
using Domain.Common;

namespace Domain.BoundedContexts.Enrollment.Entities
{
	public class CourseEnrollment : AggregateRoot
	{
		public StudentId StudentId { get; private set; } = null!;
		public CourseId CourseId { get; private set; } = null!;

		public Progress Progress { get; private set; } = null!;
		public bool IsCompleted { get; private set; }

		protected CourseEnrollment() { } // EF

		private CourseEnrollment(StudentId studentId, CourseId courseId)
		{
			if (studentId == null) throw new DomainException("StudentId is required");
			if (courseId == null) throw new DomainException("CourseId is required");

			Id = Guid.NewGuid();
			StudentId = studentId;
			CourseId = courseId;

			Progress = Progress.Create();
			IsCompleted = false;

			AddDomainEvent(new StudentEnrolled(Id, studentId.Value, courseId.Value));
		}

		public static CourseEnrollment Enroll(
			StudentId studentId,
			CourseId courseId)
		{
			if (studentId == null)
				throw new DomainException("StudentId is required");

			if (courseId == null)
				throw new DomainException("CourseId is required");

			return new CourseEnrollment(studentId, courseId);
		}

		public void CompleteLesson()
		{
			if (IsCompleted)
				throw new DomainException("The course is already completed.");

			Progress.Advance();

			AddDomainEvent(new LessonCompleted(Id, Progress.Percentage.Value));

			if (Progress.Percentage.Value == 100)
			{
				IsCompleted = true;
				AddDomainEvent(new CourseCompleted(Id));
			}
		}
	}
}