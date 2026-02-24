using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects
{
    public record CourseDescription(string Value) : IValueObject<CourseDescription, string>
    {
        public static CourseDescription From(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Course description cannot be empty.");

            return new CourseDescription(value);
        }
    }
}
