using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
    public record ExpirationDate : IValueObject
    {
        public DateTime Value { get; init; }

        protected ExpirationDate() { }

        public ExpirationDate(DateTime value)
        {
            if (value == DateTime.MinValue)
                throw new DomainException("ExpirationDate cannot be empty.");

            Value = value;
        }
    }
}