using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public record SuspendStudentCommand(Guid StudentId) : ICommand;
}
