using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.Events
{
    public class ModuleAddedToCourse : IDomainEvent
    {
        public Guid CourseId { get; private set; }
        public Guid ModuleId { get; private set; }
        public DateTime OccurredOn { get; private set; }

        public ModuleAddedToCourse(Guid courseId, Guid moduleId)
        {
            CourseId = courseId;
            ModuleId = moduleId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}