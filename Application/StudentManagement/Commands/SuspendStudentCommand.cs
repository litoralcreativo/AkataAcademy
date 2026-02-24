using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public record SuspendStudentCommand(Guid StudentId) : ICommand<StudentStatus>;
}
