using Domain.Common;

namespace Domain.BoundedContexts.Enrollment.Events
{
	public class StudentEnrolled : IDomainEvent
	{
		public Guid EnrollmentId { get; private set; }
		public Guid StudentId { get; private set; }
		public Guid CourseId { get; private set; }
		public DateTime OccurredOn { get; private set; }

		public StudentEnrolled(Guid enrollmentId, Guid studentId, Guid courseId)
		{
			EnrollmentId = enrollmentId;
			StudentId = studentId;
			CourseId = courseId;
			OccurredOn = DateTime.UtcNow;
		}
	}
}