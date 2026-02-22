using AkataAcademy.Domain.BoundedContexts.StudentManagement.Entities;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;
using static AkataAcademy.UnitTests.Domain.StudentManagement.StudentManagementTestElements;

namespace AkataAcademy.UnitTests.Domain.StudentManagement
{
	public class StudentPersonalInfoTests
	{
		[Theory]
		[MemberData(nameof(ValidStudentData), MemberType = typeof(StudentManagementTestElements))]
		public void UpdatePersonalInfo_WithValidData_ShouldSucceed(FullName name, Email email, DateOfBirth dob)
		{
			var student = Student.Register(name, email, dob).Value;
			var newName = new FullName("NewFirstName", "NewLastName");
			var newEmail = Email.From("newemail@mail.com");
			var newDob = DateOfBirth.From(DateTime.UtcNow.AddYears(-20));
			student.UpdatePersonalInfo(newName, newEmail, newDob);
			Assert.Equal(newName.ToString(), student.Name.ToString());
			Assert.Equal(newEmail.Value, student.Email.Value);
			Assert.Equal(newDob.Value, student.DateOfBirth.Value);
		}
	}
}
