using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
    public record IssueDate(DateTime Value) : IValueObject<IssueDate, DateTime>
    {
        public static IssueDate From(DateTime value)
        {
            if (value == DateTime.MinValue)
                throw new DomainException("IssueDate cannot be empty.");

            return new IssueDate(value);
        }
    }
}