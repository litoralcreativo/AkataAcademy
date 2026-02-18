using Domain.Common;

namespace Domain.BoundedContexts.Enrollment.Events
{
	public class CourseCompleted : IDomainEvent
	{
		public Guid EnrollmentId { get; private set; }
		public DateTime OccurredOn { get; private set; }

		public CourseCompleted(Guid enrollmentId)
		{
			EnrollmentId = enrollmentId;
			OccurredOn = DateTime.UtcNow;
		}
	}
}