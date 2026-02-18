using Domain.Common;

namespace Domain.BoundedContexts.Catalog.ValueObjects {
  public class Duration : ValueObject
  {
        public int Minutes { get; private set; }

        protected Duration() { } // EF

        public Duration(int minutes)
        {
            if (minutes <= 0)
                throw new DomainException("La duración debe ser mayor a 0");

            Minutes = minutes;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Minutes;
        }
  }
} 