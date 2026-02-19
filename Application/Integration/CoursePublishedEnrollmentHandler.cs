using AkataAcademy.Application.Common;
using AkataAcademy.Application.Integration;

namespace AkataAcademy.Application.Enrollment.Events
{
	public class CoursePublishedEnrollmentHandler : IIntegrationEventHandler<CoursePublishedIntegrationEvent>
	{
		public async Task Handle(CoursePublishedIntegrationEvent @event)
		{
			// Inscriptions enabling logic here
			Console.WriteLine($"\t[IE-Enrollment] Published Course");
			await Task.CompletedTask;
		}
	}
}
