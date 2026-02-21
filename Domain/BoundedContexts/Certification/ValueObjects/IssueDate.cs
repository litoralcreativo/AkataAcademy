using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
  public record IssueDate : IValueObject
  {
    public DateTime Value { get; init; }

    protected IssueDate() { }

    public IssueDate(DateTime value)
    {
      if (value == DateTime.MinValue)
        throw new DomainException("IssueDate cannot be empty.");

      Value = value;
    }
  }
}