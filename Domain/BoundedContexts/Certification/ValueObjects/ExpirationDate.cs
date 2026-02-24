using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
    public record ExpirationDate(DateTime Value) : IValueObject
    {
        public static ExpirationDate From(DateTime value)
        {
            if (value == DateTime.MinValue)
                throw new DomainException("ExpirationDate cannot be empty.");
            return new ExpirationDate(value);
        }
    }
}

