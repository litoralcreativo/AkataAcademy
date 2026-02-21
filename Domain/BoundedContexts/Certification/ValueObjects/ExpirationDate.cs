using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
    public record ExpirationDate : IValueObject
    {
        public DateTime Value { get; }

        private ExpirationDate(DateTime value)
        {
            Value = value;
        }

        public static ExpirationDate From(DateTime value)
        {
            if (value == DateTime.MinValue)
                throw new DomainException("ExpirationDate cannot be empty.");
            return new ExpirationDate(value);
        }
    }
}

