using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public record UpdateStudentCommand(
		Guid StudentId,
		string FirstName,
		string LastName,
		string Email,
		DateTime DateOfBirth) : ICommand<StudentStatus>;
}
