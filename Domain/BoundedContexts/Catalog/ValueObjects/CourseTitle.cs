using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects
{
    public class CourseTitle : ValueObject
    {
        public string Value { get; private set; } = string.Empty;

        protected CourseTitle() { } // EF

        public CourseTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Title cannot be empty.");

            if (value.Length > 100)
                throw new DomainException("Title is too long. Maximum length is 100 characters.");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
