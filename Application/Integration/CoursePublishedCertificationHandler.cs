using AkataAcademy.Application.Common;
using AkataAcademy.Application.Integration;

namespace AkataAcademy.Application.Certification.Events
{
	public class CoursePublishedCertificationHandler : IIntegrationEventHandler<CoursePublishedIntegrationEvent>
	{
		public async Task Handle(CoursePublishedIntegrationEvent @event)
		{
			// Assessments preparation logic here
			Console.WriteLine($"\t[IE-Certification] Published Course");
			await Task.CompletedTask;
		}
	}
}
