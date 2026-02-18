using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects
{
	public class StudentId : ValueObject
	{
		public Guid Value { get; private set; }

		protected StudentId() { }

		public StudentId(Guid value)
		{
			if (value == Guid.Empty)
				throw new DomainException("Invalid StudentId. Guid cannot be empty.");

			Value = value;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}