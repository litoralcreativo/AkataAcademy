using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Common.Integration
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
