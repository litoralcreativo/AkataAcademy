using AkataAcademy.Domain.BoundedContexts.StudentManagement.Entities;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.Repositories;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AkataAcademy.Infrastructure.Persistence.Repositories
{
	public class StudentRepository : IStudentRepository
	{
		private readonly ApplicationDbContext _context;

		public StudentRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public Task<bool> Exists(Guid id)
		{
			return _context.Students.AnyAsync(s => s.Id == id);
		}

		public async Task<Student?> GetByIdAsync(Guid id)
		{
			return await _context.Students.SingleOrDefaultAsync(s => s.Id == id);
		}

		public async Task<Student?> GetByEmailAsync(Email email)
		{
			return await _context.Students.SingleOrDefaultAsync(s => s.Email == email);
		}

		public Task AddAsync(Student student)
		{
			_context.Students.Add(student);
			return Task.CompletedTask;
		}

		public Task RemoveAsync(Student student)
		{
			_context.Students.Remove(student);
			return Task.CompletedTask;
		}
	}
}
