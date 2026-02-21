using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
    public record CourseId : IValueObject
    {
        public Guid Value { get; }

        private CourseId(Guid value)
        {
            Value = value;
        }

        public static CourseId From(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("Invalid CourseId. Guid cannot be empty.");
            return new CourseId(value);
        }
    }
}