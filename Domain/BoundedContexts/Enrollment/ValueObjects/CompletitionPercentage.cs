using Domain.Common;

namespace Domain.BoundedContexts.Enrollment.ValueObjects
{
    public class CompletionPercentage : ValueObject
    {
        public int Value { get; private set; }

        protected CompletionPercentage() { }

        public CompletionPercentage(int value)
        {
            if (value < 0 || value > 100)
                throw new DomainException("Invalid completion percentage. Must be between 0 and 100.");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}