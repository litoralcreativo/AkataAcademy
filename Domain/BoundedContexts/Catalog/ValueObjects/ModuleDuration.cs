using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects
{
    public record ModuleDuration : IValueObject
    {
        public int Minutes { get; init; }

        protected ModuleDuration() { }

        public ModuleDuration(int minutes)
        {
            if (minutes <= 0)
                throw new DomainException("Module duration must be positive.");
            Minutes = minutes;
        }
    }
}