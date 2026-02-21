using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
    public record IssueDate : IValueObject
    {
        public DateTime Value { get; }

        private IssueDate(DateTime value)
        {
            Value = value;
        }

        public static IssueDate From(DateTime value)
        {
            if (value == DateTime.MinValue)
                throw new DomainException("IssueDate cannot be empty.");

            return new IssueDate(value);
        }
    }
}