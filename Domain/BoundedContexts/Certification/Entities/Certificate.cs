using Domain.BoundedContexts.Certification.Events;
using Domain.BoundedContexts.Certification.ValueObjects;
using Domain.Common;

namespace Domain.BoundedContexts.Certification.Entities
{
    public class Certificate : AggregateRoot
    {
        public StudentId StudentId { get; private set; } = null!;
        public CourseId CourseId { get; private set; } = null!;
        public IssueDate IssuedOn { get; private set; } = null!;
        public ExpirationDate ExpiresOn { get; private set; } = null!;

        protected Certificate() { } // EF

        private Certificate(
            StudentId studentId,
            CourseId courseId,
            IssueDate issuedOn,
            ExpirationDate expiresOn)
        {
            if (studentId == null) throw new DomainException("StudentId requerido");
            if (courseId == null) throw new DomainException("CourseId requerido");
            if (issuedOn == null) throw new DomainException("IssuedOn requerido");
            if (expiresOn == null) throw new DomainException("ExpiresOn requerido");

            if (expiresOn.Value <= issuedOn.Value)
                throw new DomainException("La fecha de expiración debe ser posterior a la de emisión");

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

        /// <summary>
        /// Factory method de dominio.
        /// Expresa la intención de emitir un certificado.
        /// </summary>
        public static Certificate Issue(
            StudentId studentId,
            CourseId courseId,
            IssueDate issuedOn,
            ExpirationDate expiresOn)
        {
            if (studentId == null) throw new DomainException("StudentId requerido");
            if (courseId == null) throw new DomainException("CourseId requerido");
            if (issuedOn == null) throw new DomainException("IssuedOn requerido");
            if (expiresOn == null) throw new DomainException("ExpiresOn requerido");

            return new Certificate(
                studentId,
                courseId,
                issuedOn,
                expiresOn);
        }

        public bool IsExpired(DateTime currentDate)
        {
            return currentDate > ExpiresOn.Value;
        }
    }
}