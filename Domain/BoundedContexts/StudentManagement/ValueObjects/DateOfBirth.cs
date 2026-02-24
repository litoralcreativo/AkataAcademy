using AkataAcademy.Domain.Common;
using System;

namespace AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects
{
	public record DateOfBirth(DateTime Value) : IValueObject<DateOfBirth, DateTime>
	{
		public static DateOfBirth From(DateTime value)
		{
			// Convertir a UTC si no lo es
			var utcValue = value.Kind == DateTimeKind.Unspecified
				? DateTime.SpecifyKind(value, DateTimeKind.Utc)
				: value.ToUniversalTime();

			if (utcValue > DateTime.UtcNow)
				throw new DomainException("Date of birth cannot be in the future.");
			return new DateOfBirth(utcValue);
		}
	}
}
