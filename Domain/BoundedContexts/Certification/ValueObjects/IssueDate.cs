using Domain.Common;

namespace Domain.BoundedContexts.Certification.ValueObjects
{
  public class IssueDate : ValueObject
  {
    public DateTime Value { get; private set; }

    protected IssueDate() { }

    public IssueDate(DateTime value)
    {
      if (value == DateTime.MinValue)
        throw new DomainException("IssueDate cannot be empty.");

      Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return Value;
    }
  }
}