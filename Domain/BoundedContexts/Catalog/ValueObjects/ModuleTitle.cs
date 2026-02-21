using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects
{
    public record ModuleTitle : IValueObject
    {
        public string Value { get; init; } = string.Empty;

        protected ModuleTitle() { }

        public ModuleTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Module title cannot be empty.");
            Value = value;
        }
    }
}