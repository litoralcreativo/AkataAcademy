using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
    public record StudentId : IValueObject
    {
        public Guid Value { get; init; }

        protected StudentId() { }

        public StudentId(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("Invalid StudentId. Guid cannot be empty.");

            Value = value;
        }
    }
}