using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects
{
	public record class FullName(string FirstName, string LastName) : IValueObject
	{
		public static FullName From(string firstName, string lastName)
		{
			if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2 || firstName.Length > 50)
				throw new DomainException($"FirstName debe tener entre 2 y 50 caracteres.");
			if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2 || lastName.Length > 50)
				throw new DomainException($"LastName debe tener entre 2 y 50 caracteres.");

			return new FullName(firstName, lastName);
		}

		public override string ToString() => $"{FirstName} {LastName}";
	}
}
