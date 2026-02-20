using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.Events
{
    public class CoursePublished : IDomainEvent
    {
        public Guid CourseId { get; private set; }
        public DateTime OccurredOn { get; private set; }

        public CoursePublished(Guid courseId)
        {
            CourseId = courseId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}