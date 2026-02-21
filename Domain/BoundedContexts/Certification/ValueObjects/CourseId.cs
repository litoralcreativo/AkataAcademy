using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
    public record CourseId : IValueObject
    {
        public Guid Value { get; init; }

        protected CourseId() { }

        public CourseId(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("Invalid CourseId. Guid cannot be empty.");

            Value = value;
        }
    }
}