using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Catalog.Events;
using AkataAcademy.Application.Common.Integration;

namespace AkataAcademy.Application.Catalog.Events
{
	public class CourseCreatedDomainEventHandler : IDomainEventHandler<CourseCreated>
	{
		private readonly IEventBus _eventBus;

		public CourseCreatedDomainEventHandler(IEventBus eventBus)
		{
			_eventBus = eventBus;
		}

		public async Task Handle(CourseCreated domainEvent)
		{
			Console.WriteLine($"\t[DE-Catalog] Published Course");

			var integrationEvent = new CoursePublishedIntegrationEvent(domainEvent.CourseId, DateTime.UtcNow);
			await _eventBus.Publish(integrationEvent);
		}
	}
}