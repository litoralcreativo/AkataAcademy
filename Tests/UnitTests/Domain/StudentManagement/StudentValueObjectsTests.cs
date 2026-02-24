using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;
using AkataAcademy.Domain.Common;
using static AkataAcademy.UnitTests.Domain.StudentManagement.StudentManagementTestElements;

namespace AkataAcademy.UnitTests.Domain.StudentManagement
{
	public class StudentValueObjectsTests
	{
		[Theory]
		[MemberData(nameof(ValidFullNamesData), MemberType = typeof(StudentManagementTestElements))]
		public void FullName_ToString_ReturnsCorrectFormat(FullName name)
		{
			Assert.Equal($"{name.FirstName} {name.LastName}", name.ToString());
		}

		[Theory]
		[MemberData(nameof(ValidEmailsData), MemberType = typeof(StudentManagementTestElements))]
		public void Email_FromValidFormat_ShouldSucceed(Email email)
		{
			Assert.Equal(email.Value, email.ToString());
			Assert.Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", email.Value);
		}

		[Theory]
		[MemberData(nameof(ValidDateOfBirthsData), MemberType = typeof(StudentManagementTestElements))]
		public void DateOfBirth_FromValidDate_ShouldSucceed(DateOfBirth dob)
		{
			Assert.Equal(dob.Value.ToString(), dob.Value.ToString());
		}

		[Fact]
		public void DateOfBirth_FromInvalidDate_ShouldThrow()
		{
			Assert.Throws<DomainException>(() => DateOfBirth.From(DateTime.UtcNow + TimeSpan.FromDays(1)));
		}

		[Fact]
		public void StudentStatus_FromInvalidValue_ShouldThrow()
		{
			Assert.Throws<ArgumentException>(() => StudentStatus.From("Unknown"));
		}

		[Theory]
		[InlineData("Active", true)]
		[InlineData("Suspended", false)]
		public void StudentStatus_CanEnroll_ReturnsExpected(string status, bool canEnroll)
		{
			var s = StudentStatus.From(status);
			Assert.Equal(canEnroll, s.CanEnroll());
		}

		[Theory]
		[MemberData(nameof(InvalidFullNamesData), MemberType = typeof(StudentManagementTestElements))]
		public void FullName_FromInvalidData_ShouldThrow(string firstName, string lastName)
		{
			Assert.Throws<DomainException>(() => FullName.From((firstName, lastName)));
		}

		[Theory]
		[MemberData(nameof(InvalidEmailsData), MemberType = typeof(StudentManagementTestElements))]
		public void Email_FromInvalidData_ShouldThrow(string email)
		{
			Assert.Throws<DomainException>(() => Email.From(email));
		}
	}
}
