using Domain.Common;

namespace Domain.BoundedContexts.Catalog.ValueObjects
{
    public class Duration : ValueObject
    {
        public int Minutes { get; private set; }

        protected Duration() { } // EF

        public Duration(int minutes)
        {
            if (minutes <= 0)
                throw new DomainException("Duration must be greater than zero.");

            Minutes = minutes;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Minutes;
        }
    }
}