using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Certification.Entities;

namespace AkataAcademy.Application.Certification.DTOs
{
	public class CertificateDto : DTO<Certificate, Guid>
	{
		public Guid Id { get; set; }
		public Guid StudentId { get; set; }
		public Guid CourseId { get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime ExpirationDate { get; set; }
		public bool IsExpired { get; set; }
	}
}
