using System.Text.RegularExpressions;

namespace AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects
{
	public record Email(string Value)
	{
		public static Email From(string value)
		{
			if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
				throw new ArgumentException("Invalid email format.");
			return new Email(value);
		}
		public override string ToString() => Value;
	}
}
