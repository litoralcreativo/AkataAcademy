using Domain.Common;

namespace Domain.BoundedContexts.Enrollment.Events
{
	public class LessonCompleted : IDomainEvent
	{
		public Guid EnrollmentId { get; private set; }
		public int CurrentPercentage { get; private set; }
		public DateTime OccurredOn { get; private set; }

		public LessonCompleted(Guid enrollmentId, int percentage)
		{
			EnrollmentId = enrollmentId;
			CurrentPercentage = percentage;
			OccurredOn = DateTime.UtcNow;
		}
	}
}