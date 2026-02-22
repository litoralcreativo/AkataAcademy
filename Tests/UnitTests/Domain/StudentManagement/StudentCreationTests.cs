using AkataAcademy.Domain.BoundedContexts.StudentManagement.Entities;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.DomainEvents;
using static AkataAcademy.UnitTests.Domain.StudentManagement.StudentManagementTestElements;

namespace AkataAcademy.UnitTests.Domain.StudentManagement
{
	public class StudentCreationTests
	{
		[Theory]
		[MemberData(nameof(ValidStudentData), MemberType = typeof(StudentManagementTestElements))]
		public void Register_WithNullName_ShouldFail(FullName name, Email email, DateOfBirth dob)
		{
			var result = Student.Register(null, email, dob);
			Assert.True(result.IsFailure);
		}

		[Theory]
		[MemberData(nameof(ValidStudentData), MemberType = typeof(StudentManagementTestElements))]
		public void Register_WithNullEmail_ShouldFail(FullName name, Email email, DateOfBirth dob)
		{
			var result = Student.Register(name, null, dob);
			Assert.True(result.IsFailure);
		}

		[Theory]
		[MemberData(nameof(ValidStudentData), MemberType = typeof(StudentManagementTestElements))]
		public void Register_WithNullDateOfBirth_ShouldFail(FullName name, Email email, DateOfBirth dob)
		{
			var result = Student.Register(name, email, null);
			Assert.True(result.IsFailure);
		}

		[Theory]
		[MemberData(nameof(ValidStudentData), MemberType = typeof(StudentManagementTestElements))]
		public void Register_WithValidData_ShouldSucceed(FullName name, Email email, DateOfBirth dob)
		{
			var result = Student.Register(name, email, dob);
			Assert.True(result.IsSuccess);
			Assert.NotNull(result.Value);
			Assert.Equal(StudentStatus.Active, result.Value.Status);
			var student = result.Value;
			Assert.Contains(student.DomainEvents, e => e is StudentRegistered);
		}
	}
}
