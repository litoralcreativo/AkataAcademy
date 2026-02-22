using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public record DeleteStudentCommand(Guid StudentId) : ICommand;
}
