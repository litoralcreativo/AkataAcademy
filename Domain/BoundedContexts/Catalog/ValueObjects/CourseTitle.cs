using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects
{
    public record CourseTitle : IValueObject
    {
        public string Value { get; }

        private CourseTitle(string value)
        {
            Value = value;
        }

        public static CourseTitle From(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Course title cannot be empty.");

            if (value.Length > 100)
                throw new DomainException("Title is too long. Maximum length is 100 characters.");

            return new CourseTitle(value);
        }
    }
}