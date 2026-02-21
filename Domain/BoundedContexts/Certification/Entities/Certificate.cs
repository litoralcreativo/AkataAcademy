using AkataAcademy.Domain.BoundedContexts.Certification.Events;
using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.Entities
{
    public class Certificate : AggregateRoot
    {
        public StudentId StudentId { get; private set; } = null!;
        public CourseId CourseId { get; private set; } = null!;
        public IssueDate IssuedOn { get; private set; } = null!;
        public ExpirationDate ExpiresOn { get; private set; } = null!;

        protected Certificate()
        {
            StudentId = default!;
            CourseId = default!;
            IssuedOn = default!;
            ExpiresOn = default!;
        } // EF

        private Certificate(
            StudentId studentId,
            CourseId courseId,
            IssueDate issuedOn,
            ExpirationDate expiresOn)
        {
            if (studentId == null) throw new DomainException("StudentId is required");
            if (courseId == null) throw new DomainException("CourseId is required");
            if (issuedOn == null) throw new DomainException("IssuedOn is required");
            if (expiresOn == null) throw new DomainException("ExpiresOn is required");

            if (expiresOn.Value <= issuedOn.Value)
                throw new DomainException("ExpirationDate must be after IssueDate");

            Id = Guid.NewGuid();
            StudentId = studentId;
            CourseId = courseId;
            IssuedOn = issuedOn;
            ExpiresOn = expiresOn;

            AddDomainEvent(new CertificateIssued(
                Id,
                studentId.Value,
                courseId.Value,
                issuedOn.Value));
        }

        public static Result<Certificate> Issue(
            StudentId studentId,
            CourseId courseId,
            IssueDate issuedOn,
            ExpirationDate expiresOn)
        {
            try
            {
                return new Certificate(
                studentId,
                courseId,
                issuedOn,
                expiresOn);
            }
            catch (DomainException ex)
            {
                return Result.Failure<Certificate>(Error.Validation(ErrorCodes.Certificate.Creation, ex.Message));
            }
            catch (Exception ex)
            {
                return Result.Failure<Certificate>(Error.Failure("Certificate", ex.Message));
            }
        }

        public bool IsExpired(DateTime currentDate)
        {
            return currentDate > ExpiresOn.Value;
        }
    }
}