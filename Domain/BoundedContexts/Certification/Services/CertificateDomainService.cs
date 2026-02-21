using AkataAcademy.Domain.BoundedContexts.Certification.Entities;
using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;

namespace AkataAcademy.Domain.BoundedContexts.Certification.Services
{
	/// <summary>
	/// Domain service for business rules related to certificate issuance.
	/// </summary>
	public class CertificateDomainService
	{
		/// <summary>
		/// Determines if a certificate can be issued based on Certification BC rules.
		/// </summary>
		/// <param name="existingCertificate">An existing certificate for the student and course, or null if none exists.</param>
		/// <param name="issueDate">The date the certificate would be issued.</param>
		/// <param name="expirationDate">The expiration date of the certificate.</param>
		/// <returns>True if the certificate can be issued, false otherwise.</returns>
		public bool CanIssueCertificate(
			Certificate? existingCertificate,
			IssueDate issueDate,
			ExpirationDate expirationDate)
		{
			// Rule: Only one certificate per student per course. If existsingCertificate is not null, it means a certificate already exists for this student and course.
			if (existingCertificate is not null)
				return false;

			// Rule: Expiration date must be after issue date.
			if (expirationDate.Value <= issueDate.Value)
				return false;

			return true;
		}
	}
}