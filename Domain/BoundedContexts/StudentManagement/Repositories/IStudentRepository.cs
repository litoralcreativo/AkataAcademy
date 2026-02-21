using AkataAcademy.Domain.BoundedContexts.StudentManagement.Entities;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;
using AkataAcademy.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AkataAcademy.Domain.BoundedContexts.StudentManagement.Repositories
{
	public interface IStudentRepository : IRepository<Student>
	{
		Task<Student?> GetByEmailAsync(Email email);
	}
}