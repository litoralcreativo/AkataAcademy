using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Integration
{
	public class CoursePublishedIntegrationEvent : IIntegrationEvent
	{
		public Guid CourseId { get; }
		public DateTime OccurredOn { get; }

		public CoursePublishedIntegrationEvent(Guid courseId, DateTime occurredOn)
		{
			CourseId = courseId;
			OccurredOn = occurredOn;
		}
	}
}