using AkataAcademy.Domain.BoundedContexts.Catalog.Entities;
using AkataAcademy.Domain.BoundedContexts.Catalog.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AkataAcademy.Infrastructure.Persistence
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.Courses.AnyAsync(c => c.Id == id);
        }

        public async Task<Course?> GetByIdAsync(Guid id)
        {
            return await _context.Courses
                .Include(c => c.Modules)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public Task AddAsync(Course course)
        {
            return Task.FromResult(_context.Courses.Add(course) != null);
        }

        public Task RemoveAsync(Course course)
        {
            return Task.FromResult(_context.Courses.Remove(course) != null);
        }
    }
}
