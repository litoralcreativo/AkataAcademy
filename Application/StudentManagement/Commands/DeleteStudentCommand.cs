using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public record DeleteStudentCommand(Guid StudentId) : ICommand<StudentStatus>;
}
