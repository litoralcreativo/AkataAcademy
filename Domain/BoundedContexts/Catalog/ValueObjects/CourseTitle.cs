using Domain.Common;

namespace Domain.BoundedContexts.Catalog.ValueObjects
{
    public class CourseTitle : ValueObject
    {
        public string Value { get; private set; } = string.Empty;

        protected CourseTitle() { } // EF

        public CourseTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("El título no puede estar vacío");

            if (value.Length > 100)
                throw new DomainException("El título es demasiado largo");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
