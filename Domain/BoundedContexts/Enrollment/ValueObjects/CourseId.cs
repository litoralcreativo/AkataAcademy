using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects
{
	public record CourseId : IValueObject
	{
		public Guid Value { get; init; }

		// Parameterless constructor for EF Core
		protected CourseId() { }

		public CourseId(Guid value)
		{
			if (value == Guid.Empty)
				throw new DomainException("Invalid CourseId. Guid cannot be empty.");

			Value = value;
		}
	}
}