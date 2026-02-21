using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
    public record CourseId(Guid Value) : IValueObject
    {
        protected CourseId() : this(Guid.Empty) { }

        public static CourseId From(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("Invalid CourseId. Guid cannot be empty.");
            return new CourseId(value);
        }
    }
}