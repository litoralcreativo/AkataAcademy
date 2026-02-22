using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public record ActivateStudentCommand(Guid StudentId) : ICommand;
}
