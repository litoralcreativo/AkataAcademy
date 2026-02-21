using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects
{
    public record ModuleDuration : IValueObject
    {
        public int Minutes { get; }

        private ModuleDuration(int minutes)
        {
            Minutes = minutes;
        }

        public static ModuleDuration From(int minutes)
        {
            if (minutes <= 0)
                throw new DomainException("Module duration must be positive.");

            return new ModuleDuration(minutes);
        }
    }
}