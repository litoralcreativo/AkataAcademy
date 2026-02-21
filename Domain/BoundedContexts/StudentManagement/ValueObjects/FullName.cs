namespace AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects
{
	public record FullName(string FirstName, string LastName)
	{
		public override string ToString() => $"{FirstName} {LastName}";
	}
}
