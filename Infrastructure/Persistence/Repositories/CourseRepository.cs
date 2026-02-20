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

        public async Task<Course?> GetByIdAsync(Guid id)
        {
            return await _context.Courses
                .Include(c => c.Modules)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public void Add(Course course)
        {
            _context.Courses.Add(course);
        }

        public void Remove(Course course)
        {
            _context.Courses.Remove(course);
        }

        public bool Exists(Guid id)
        {
            return _context.Courses.Any(c => c.Id == id);
        }
    }
}
