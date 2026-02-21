using System.Windows.Input;
using AkataAcademy.Application.Common;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public record RegisterStudentCommand(
		string FirstName,
		string LastName,
		string Email,
		DateTime DateOfBirth
	) : ICommand<Guid>;
}
