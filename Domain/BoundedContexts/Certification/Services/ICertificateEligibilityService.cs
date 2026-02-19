using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;

namespace AkataAcademy.Domain.BoundedContexts.Certification.Services
{
    public interface ICertificateEligibilityService
    {
        bool IsEligible(StudentId studentId, CourseId courseId);
    }
}