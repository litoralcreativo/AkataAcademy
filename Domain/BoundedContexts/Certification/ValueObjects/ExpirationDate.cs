using Domain.Common;

namespace Domain.BoundedContexts.Certification.ValueObjects
{
    public class ExpirationDate : ValueObject
    {
        public DateTime Value { get; private set; }

        protected ExpirationDate() { }

        public ExpirationDate(DateTime value)
        {
            if (value == DateTime.MinValue)
                throw new DomainException("ExpirationDate cannot be empty.");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}