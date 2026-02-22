using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public record UpdateStudentCommand(
		Guid StudentId,
		string FirstName,
		string LastName,
		string Email,
		DateTime DateOfBirth) : ICommand;
}
