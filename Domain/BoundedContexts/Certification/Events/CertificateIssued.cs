using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.Events
{
    public class CertificateIssued : IDomainEvent
    {
        public Guid CertificateId { get; private set; }
        public Guid StudentId { get; private set; }
        public Guid CourseId { get; private set; }
        public DateTime IssuedOn { get; private set; }
        public DateTime OccurredOn { get; private set; }

        public CertificateIssued(
            Guid certificateId,
            Guid studentId,
            Guid courseId,
            DateTime issuedOn)
        {
            CertificateId = certificateId;
            StudentId = studentId;
            CourseId = courseId;
            IssuedOn = issuedOn;
            OccurredOn = DateTime.UtcNow;
        }
    }
}