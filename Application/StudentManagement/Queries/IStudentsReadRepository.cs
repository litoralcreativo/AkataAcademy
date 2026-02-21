
using AkataAcademy.Application.Common;
using AkataAcademy.Application.StudentManagement.DTOs;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.Entities;

namespace AkataAcademy.Application.StudentManagement.Queries
{
	public interface IStudentReadRepository : IReadRepository<Student, StudentDto, Guid>
	{
		Task<IEnumerable<StudentDto>> GetAllStudents();
	}
}