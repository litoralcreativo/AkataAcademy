using System;
using System.Collections.Generic;
using System.Linq;
using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Catalog.Queries;
using Microsoft.EntityFrameworkCore;

namespace AkataAcademy.Infrastructure.Persistence
{
    public class CourseReadRepository : ICourseReadRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CourseDto?> GetById(Guid id)
        {
            return await _context.Courses
                .Where(c => c.Id == id)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title.Value,
                    Description = c.Description.Value,
                    IsPublished = c.IsPublished,
                    Modules = c.Modules
                        .Select(m => new CourseModuleDto
                        {
                            Id = m.Id,
                            Title = m.Title.Value,
                            DurationMinutes = m.Duration.Minutes
                        })
                        .ToList()
                })
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetPublishedCourses()
        {
            return await _context.Courses
                .Where(c => c.IsPublished)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title.Value,
                    Description = c.Description.Value,
                    IsPublished = c.IsPublished,
                    Modules = c.Modules
                        .Select(m => new CourseModuleDto
                        {
                            Id = m.Id,
                            Title = m.Title.Value,
                            DurationMinutes = m.Duration.Minutes
                        })
                        .ToList()
                })
                .ToListAsync();
        }
    }
}
