using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects
{
    public record CourseDescription : IValueObject
    {
        public string Value { get; init; } = string.Empty;

        protected CourseDescription() { }

        public CourseDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Course description cannot be empty.");
            Value = value;
        }
    }
}
