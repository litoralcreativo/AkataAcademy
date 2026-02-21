using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Common.Integration
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
