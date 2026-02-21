using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.Events
{
    public record CertificateIssued(
        Guid CertificateId,
        Guid StudentId,
        Guid CourseId,
        DateTime IssuedOn,
        DateTime OccurredOn
    ) : IDomainEvent
    {
        public CertificateIssued(Guid certificateId, Guid studentId, Guid courseId, DateTime issuedOn)
            : this(certificateId, studentId, courseId, issuedOn, DateTime.UtcNow) { }
    }
}