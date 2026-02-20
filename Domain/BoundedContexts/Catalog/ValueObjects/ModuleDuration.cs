using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects
{
    public class ModuleDuration : ValueObject
    {
        public int Minutes { get; private set; }

        protected ModuleDuration() { } // EF

        public ModuleDuration(int minutes)
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