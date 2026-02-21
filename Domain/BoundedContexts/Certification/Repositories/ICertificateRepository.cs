using AkataAcademy.Domain.Common;
using AkataAcademy.Domain.BoundedContexts.Certification.Entities;
using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;

namespace AkataAcademy.Domain.BoundedContexts.Certification.Repositories
{
    public interface ICertificateRepository : IRepository<Certificate>
    {
        Task<Certificate?> GetByStudentAndCourseAsync(StudentId studentId, CourseId courseId);
    }
}