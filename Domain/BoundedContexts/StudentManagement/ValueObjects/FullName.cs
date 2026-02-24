using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects
{
	public record class FullName(string FirstName, string LastName) : IValueObject<FullName, (string FirstName, string LastName)>
	{
		public static FullName From((string FirstName, string LastName) value)
		{
			if (string.IsNullOrWhiteSpace(value.FirstName) || value.FirstName.Length < 2 || value.FirstName.Length > 50)
				throw new DomainException($"FirstName debe tener entre 2 y 50 caracteres.");
			if (string.IsNullOrWhiteSpace(value.LastName) || value.LastName.Length < 2 || value.LastName.Length > 50)
				throw new DomainException($"LastName debe tener entre 2 y 50 caracteres.");

			return new FullName(value.FirstName, value.LastName);
		}

		public override string ToString() => $"{FirstName} {LastName}";
	}
}
