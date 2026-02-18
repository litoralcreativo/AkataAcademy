using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.Events
{
    public class CourseCreated : IDomainEvent
    {
        public Guid CourseId { get; private set; }
        public DateTime OccurredOn { get; private set; }

        public CourseCreated(Guid courseId)
        {
            CourseId = courseId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}