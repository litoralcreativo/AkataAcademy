using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Catalog.Events;
using AkataAcademy.Application.Integration;
using System;
using System.Threading.Tasks;

namespace AkataAcademy.Application.Catalog.Events
{
	public class CourseCreatedHandler : IDomainEventHandler<CourseCreated>
	{
		private readonly IEventBus _eventBus;

		public CourseCreatedHandler(IEventBus eventBus)
		{
			_eventBus = eventBus;
		}

		public async Task Handle(CourseCreated domainEvent)
		{
			Console.WriteLine($"\t[DE-Catalog] Published Course");

			var integrationEvent = new CoursePublishedIntegrationEvent(domainEvent.CourseId, DateTime.UtcNow);
			_eventBus.Publish(integrationEvent);

			await Task.CompletedTask;
		}
	}
}