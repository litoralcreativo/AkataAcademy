using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.Events
{
	public record CourseCreated(Guid CourseId, DateTime OccurredOn) : IDomainEvent
	{
		public CourseCreated(Guid courseId) : this(courseId, DateTime.UtcNow) { }
	}

	public record CoursePublished(Guid CourseId, DateTime OccurredOn) : IDomainEvent
	{
		public CoursePublished(Guid courseId) : this(courseId, DateTime.UtcNow) { }
	}

	public record ModuleAddedToCourse(Guid CourseId, Guid ModuleId, DateTime OccurredOn) : IDomainEvent
	{
		public ModuleAddedToCourse(Guid courseId, Guid moduleId) : this(courseId, moduleId, DateTime.UtcNow) { }
	}
}
