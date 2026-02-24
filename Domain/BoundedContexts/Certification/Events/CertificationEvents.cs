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

    /// <summary>
    /// Fired when a student completes a course and becomes eligible for certification.
    /// This event is raised by EligibilityRecord when transitioning from Pending to Eligible.
    /// </summary>
    public record StudentBecameEligible(
        Guid EligibilityRecordId,
        Guid StudentId,
        Guid CourseId,
        DateTime CompletedOn,
        DateTime OccurredOn
    ) : IDomainEvent
    {
        public StudentBecameEligible(Guid eligibilityRecordId, Guid studentId, Guid courseId, DateTime completedOn)
            : this(eligibilityRecordId, studentId, courseId, completedOn, DateTime.UtcNow) { }
    }

    /// <summary>
    /// Fired when an eligibility record is revoked.
    /// This can happen if a certificate is revoked or if duplicates are detected.
    /// </summary>
    public record EligibilityRevoked(
        Guid EligibilityRecordId,
        Guid StudentId,
        Guid CourseId,
        string Reason,
        DateTime OccurredOn
    ) : IDomainEvent
    {
        public EligibilityRevoked(Guid eligibilityRecordId, Guid studentId, Guid courseId, string reason)
            : this(eligibilityRecordId, studentId, courseId, reason, DateTime.UtcNow) { }
    }
}