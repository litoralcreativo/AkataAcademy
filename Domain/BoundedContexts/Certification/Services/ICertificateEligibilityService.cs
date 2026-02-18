using Domain.BoundedContexts.Certification.ValueObjects;

namespace Domain.BoundedContexts.Certification.Services
{
    public interface ICertificateEligibilityService
    {
        bool IsEligible(StudentId studentId, CourseId courseId);
    }
}