using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects
{
    public record ModuleTitle(string Value) : IValueObject
    {
        public static ModuleTitle From(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Module title cannot be empty.");

            return new ModuleTitle(value);
        }
    }
}