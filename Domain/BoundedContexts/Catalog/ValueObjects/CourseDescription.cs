using Domain.Common;

namespace Domain.BoundedContexts.Catalog.ValueObjects
{
    public class CourseDescription : ValueObject
    {
        public string Value { get; private set; } = string.Empty;

        protected CourseDescription() { } // EF

        public CourseDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("La descripción no puede estar vacía");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
