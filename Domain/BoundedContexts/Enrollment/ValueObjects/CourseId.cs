using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects
{
	public record CourseId(Guid Value) : IValueObject<CourseId, Guid>
	{
		public static CourseId From(Guid value)
		{
			if (value == Guid.Empty)
				throw new DomainException("Invalid CourseId. Guid cannot be empty.");
			return new CourseId(value);
		}
	}
}