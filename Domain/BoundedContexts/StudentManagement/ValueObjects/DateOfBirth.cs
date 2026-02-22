using AkataAcademy.Domain.Common;
using System;

namespace AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects
{
	public record DateOfBirth(DateTime Value)
	{
		public static DateOfBirth From(DateTime value)
		{
			if (value > DateTime.UtcNow)
				throw new DomainException("Date of birth cannot be in the future.");
			return new DateOfBirth(value);
		}
	}
}
