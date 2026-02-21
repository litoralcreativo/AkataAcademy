using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
    public record StudentId : IValueObject
    {
        public Guid Value { get; }

        private StudentId(Guid value)
        {
            Value = value;
        }

        public static StudentId From(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("Invalid StudentId. Guid cannot be empty.");

            return new StudentId(value);
        }
    }
}