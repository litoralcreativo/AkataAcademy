using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects
{
    public class ModuleTitle : ValueObject
    {
        public string Value { get; private set; } = string.Empty;

        protected ModuleTitle() { }

        public ModuleTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Title cannot be empty.");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}