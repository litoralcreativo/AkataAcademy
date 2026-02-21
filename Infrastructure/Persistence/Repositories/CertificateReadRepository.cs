using System;
using System.Collections.Generic;
using System.Linq;
using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Catalog.Queries;
using AkataAcademy.Application.Certification.DTOs;
using AkataAcademy.Application.Certification.Queries;
using Microsoft.EntityFrameworkCore;

namespace AkataAcademy.Infrastructure.Persistence
{
    public class CertificateReadRepository : ICertificateReadRepository
    {
        private readonly ApplicationDbContext _context;

        public CertificateReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CertificateDto?> GetById(Guid id)
        {
            return await _context.Certificates
                .Where(c => c.Id == id)
                .Select(c => new CertificateDto
                {
                    Id = c.Id,
                    StudentId = c.StudentId.Value,
                    CourseId = c.CourseId.Value,
                    IssueDate = c.IssuedOn.Value,
                    ExpirationDate = c.ExpiresOn.Value,
                    IsExpired = c.IsExpired(DateTime.UtcNow)
                })
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CertificateDto>> GetValidCertificates()
        {
            return await _context.Certificates
                .Where(c => c.ExpiresOn.Value > DateTime.UtcNow)
                .Select(c => new CertificateDto
                {
                    Id = c.Id,
                    StudentId = c.StudentId.Value,
                    CourseId = c.CourseId.Value,
                    IssueDate = c.IssuedOn.Value,
                    ExpirationDate = c.ExpiresOn.Value,
                    IsExpired = c.ExpiresOn.Value <= DateTime.UtcNow
                })
                .ToListAsync();
        }
    }
}
