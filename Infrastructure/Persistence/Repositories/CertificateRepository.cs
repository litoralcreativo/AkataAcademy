using AkataAcademy.Domain.BoundedContexts.Certification.Entities;
using AkataAcademy.Domain.BoundedContexts.Certification.Repositories;
using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AkataAcademy.Infrastructure.Persistence
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly ApplicationDbContext _context;

        public CertificateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.Certificates.AnyAsync(c => c.Id == id);
        }

        public async Task<Certificate?> GetByIdAsync(Guid id)
        {
            return await _context.Certificates.FindAsync(id);
        }

        public Task AddAsync(Certificate course)
        {
            return Task.FromResult(_context.Certificates.Add(course) != null);
        }

        public Task RemoveAsync(Certificate course)
        {
            return Task.FromResult(_context.Certificates.Remove(course) != null);
        }

        public async Task<Certificate?> GetByStudentAndCourseAsync(StudentId studentId, CourseId courseId)
        {
            return await _context.Certificates
                .Where(c => c.StudentId.Value == studentId.Value && c.CourseId.Value == courseId.Value)
                .SingleOrDefaultAsync();
        }
    }
}
