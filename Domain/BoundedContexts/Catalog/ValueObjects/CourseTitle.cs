using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects
{
    public record CourseTitle : IValueObject
    {
        public string Value { get; init; } = string.Empty;

        protected CourseTitle() { }

        public CourseTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Course title cannot be empty.");

            if (value.Length > 100)
                throw new DomainException("Title is too long. Maximum length is 100 characters.");

            Value = value;
        }
    }
}
