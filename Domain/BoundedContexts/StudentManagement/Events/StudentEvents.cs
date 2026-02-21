using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.StudentManagement.DomainEvents
{
	public record StudentRegistered(Guid CourseId, DateTime OccurredOn) : IDomainEvent
	{
		public StudentRegistered(Guid CourseId) : this(CourseId, DateTime.UtcNow) { }
	}
	public record StudentUpdated(Guid StudentId, DateTime OccurredOn) : IDomainEvent
	{
		public StudentUpdated(Guid StudentId) : this(StudentId, DateTime.UtcNow) { }
	}
	public record StudentActivated(Guid StudentId, DateTime OccurredOn) : IDomainEvent
	{
		public StudentActivated(Guid StudentId) : this(StudentId, DateTime.UtcNow) { }
	}
	public record StudentSuspended(Guid StudentId, DateTime OccurredOn) : IDomainEvent
	{
		public StudentSuspended(Guid StudentId) : this(StudentId, DateTime.UtcNow) { }
	}
	public record StudentDeleted(Guid StudentId, DateTime OccurredOn) : IDomainEvent
	{
		public StudentDeleted(Guid StudentId) : this(StudentId, DateTime.UtcNow) { }
	}

}
