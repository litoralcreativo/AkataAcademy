using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
    public record StudentId(Guid Value) : IValueObject
    {
        public static StudentId From(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("Invalid StudentId. Guid cannot be empty.");

            return new StudentId(value);
        }
    }
}