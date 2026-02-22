using AkataAcademy.Domain.BoundedContexts.StudentManagement.Entities;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;

namespace AkataAcademy.UnitTests.Domain.StudentManagement
{
	public static class StudentManagementTestElements
	{
		public static readonly DateOfBirth[] ValidDateOfBirths = new[]
		{
			DateOfBirth.From(new DateTime(2000, 1, 1)),
			DateOfBirth.From(new DateTime(1995, 5, 5)),
			DateOfBirth.From(new DateTime(1980, 12, 12)),
			DateOfBirth.From(new DateTime(2010, 6, 15)),
			DateOfBirth.From(new DateTime(1999, 3, 22))
		};

		public static readonly Email[] ValidEmails = new[]
		{
			Email.From("juan@mail.com"),
			Email.From("ana@mail.com"),
			Email.From("pedro@mail.com"),
			Email.From("luis@mail.com"),
			Email.From("maria@mail.com")
		};

		public static readonly FullName[] ValidFullNames = new[]
		{
			new FullName("Juan", "Perez"),
			new FullName("Ana", "Gomez"),
			new FullName("Pedro", "Lopez"),
			new FullName("Luis", "Martinez"),
			new FullName("Maria", "Fernandez")
		};

		public static IEnumerable<object[]> ValidFullNamesData => ValidFullNames.Select(fn => new object[] { fn });
		public static IEnumerable<object[]> ValidEmailsData => ValidEmails.Select(e => new object[] { e });
		public static IEnumerable<object[]> ValidDateOfBirthsData => ValidDateOfBirths.Select(d => new object[] { d });
		public static IEnumerable<object[]> ValidStudentData =>
			ValidFullNames.Zip(ValidEmails, (fn, em) => new { fn, em })
			.Zip(ValidDateOfBirths, (pair, dob) => new object[] { pair.fn, pair.em, dob });
	}
}