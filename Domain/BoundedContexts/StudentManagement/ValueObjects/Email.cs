using AkataAcademy.Domain.Common;
using System.Text.RegularExpressions;

namespace AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects
{
	public record Email(string Value) : IValueObject<Email, string>
	{
		public static Email From(string value)
		{
			if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
				throw new DomainException("Invalid email format.");
			return new Email(value);
		}
		public override string ToString() => Value;
	}
}
