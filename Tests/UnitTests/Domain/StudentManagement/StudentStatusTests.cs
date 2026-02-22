using AkataAcademy.Domain.BoundedContexts.StudentManagement.Entities;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;
using static AkataAcademy.UnitTests.Domain.StudentManagement.StudentManagementTestElements;

namespace AkataAcademy.UnitTests.Domain.StudentManagement
{
	public class StudentStatusTests
	{
		[Theory]
		[MemberData(nameof(ValidStudentData), MemberType = typeof(StudentManagementTestElements))]
		public void Activate_FromDeleted_ShouldFail(FullName name, Email email, DateOfBirth dob)
		{
			var student = Student.Register(name, email, dob).Value;
			student.Delete();
			var previousStatus = student.Status;
			var previousEvents = student.DomainEvents.Count;
			var result = student.Activate();
			Assert.True(result.IsFailure);
			Assert.Equal(previousStatus, student.Status);
			Assert.Equal(previousEvents, student.DomainEvents.Count);
		}

		[Theory]
		[MemberData(nameof(ValidStudentData), MemberType = typeof(StudentManagementTestElements))]
		public void Activate_FromActive_ShouldFail(FullName name, Email email, DateOfBirth dob)
		{
			var student = Student.Register(name, email, dob).Value;
			var previousStatus = student.Status;
			var previousEvents = student.DomainEvents.Count;
			var result = student.Activate();
			Assert.True(result.IsFailure);
			Assert.Equal(StudentStatus.Active, student.Status);
			Assert.Equal(previousEvents, student.DomainEvents.Count);
		}

		[Theory]
		[MemberData(nameof(ValidStudentData), MemberType = typeof(StudentManagementTestElements))]
		public void Suspend_FromDeleted_ShouldFail(FullName name, Email email, DateOfBirth dob)
		{
			var student = Student.Register(name, email, dob).Value;
			student.Delete();
			var previousStatus = student.Status;
			var previousEvents = student.DomainEvents.Count;
			var result = student.Suspend();
			Assert.True(result.IsFailure);
			Assert.Equal(previousStatus, student.Status);
			Assert.Equal(previousEvents, student.DomainEvents.Count);
		}

		[Theory]
		[MemberData(nameof(ValidStudentData), MemberType = typeof(StudentManagementTestElements))]
		public void Suspend_FromActive_ShouldSucceed(FullName name, Email email, DateOfBirth dob)
		{
			var student = Student.Register(name, email, dob).Value;
			var result = student.Suspend();
			Assert.True(result.IsSuccess);
			Assert.Equal(student.Status, StudentStatus.Suspended);
			Assert.Contains(student.DomainEvents, e => e.GetType().Name == "StudentSuspended");
		}

		[Theory]
		[MemberData(nameof(ValidStudentData), MemberType = typeof(StudentManagementTestElements))]
		public void Delete_FromDeleted_ShouldFail(FullName name, Email email, DateOfBirth dob)
		{
			var student = Student.Register(name, email, dob).Value;
			student.Delete();
			var previousStatus = student.Status;
			var previousEvents = student.DomainEvents.Count;
			var result = student.Delete();
			Assert.True(result.IsFailure);
			Assert.Equal(previousStatus, student.Status);
			Assert.Equal(previousEvents, student.DomainEvents.Count);
		}

		[Theory]
		[MemberData(nameof(ValidStudentData), MemberType = typeof(StudentManagementTestElements))]
		public void Delete_FromActive_ShouldSucceed(FullName name, Email email, DateOfBirth dob)
		{
			var student = Student.Register(name, email, dob).Value;
			var result = student.Delete();
			Assert.True(result.IsSuccess);
			Assert.Equal(student.Status, StudentStatus.Deleted);
			Assert.Contains(student.DomainEvents, e => e.GetType().Name == "StudentDeleted");
		}
	}
}
