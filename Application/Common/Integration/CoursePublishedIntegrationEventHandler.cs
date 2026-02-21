namespace AkataAcademy.Application.Common.Integration
{
    public class CoursePublishedIntegrationEventHandler : IIntegrationEventHandler<CoursePublishedIntegrationEvent>
    {
        public async Task Handle(CoursePublishedIntegrationEvent @event)
        {
            // Inscriptions enabling logic here
            Console.WriteLine($"\t[IE-Enrollment] Published Course");
            await Task.CompletedTask;
        }
    }
}
