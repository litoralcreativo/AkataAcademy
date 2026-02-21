using AkataAcademy.Application.Common;
using AkataAcademy.Application.StudentManagement.DTOs;

namespace AkataAcademy.Application.StudentManagement.Queries
{
	public record GetStudentByIdQuery(Guid Id) : IQuery<StudentDto>;
}
