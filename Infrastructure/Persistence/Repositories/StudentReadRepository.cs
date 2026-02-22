using AkataAcademy.Application.StudentManagement.DTOs;
using AkataAcademy.Application.StudentManagement.Queries;
using Microsoft.EntityFrameworkCore;

namespace AkataAcademy.Infrastructure.Persistence.Repositories
{
	public class StudentReadRepository : IStudentReadRepository
	{
		private readonly ApplicationDbContext _context;

		public StudentReadRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<StudentDto?> GetById(Guid id)
		{
			return await _context.Students
				.Where(s => s.Id == id)
				.Select(s => new StudentDto
				{
					Id = s.Id,
					FirstName = s.Name.FirstName,
					LastName = s.Name.LastName,
					Email = s.Email.Value,
					DateOfBirth = s.DateOfBirth.Value,
					Status = s.Status.ToString()
				})
				.SingleOrDefaultAsync();
		}

		public async Task<IEnumerable<StudentDto>> GetAllAsync()
		{
			return await _context.Students
				.Select(s => new StudentDto
				{
					Id = s.Id,
					FirstName = s.Name.FirstName,
					LastName = s.Name.LastName,
					Email = s.Email.Value,
					DateOfBirth = s.DateOfBirth.Value,
					Status = s.Status.ToString()
				})
				.ToListAsync();
		}

		public async Task<IEnumerable<StudentDto>> GetByStatusAsync(string status)
		{
			return await _context.Students
				.Where(s => s.Status.ToString() == status)
				.Select(s => new StudentDto
				{
					Id = s.Id,
					FirstName = s.Name.FirstName,
					LastName = s.Name.LastName,
					Email = s.Email.Value,
					DateOfBirth = s.DateOfBirth.Value,
					Status = s.Status.ToString()
				})
				.ToListAsync();
		}
	}
}
