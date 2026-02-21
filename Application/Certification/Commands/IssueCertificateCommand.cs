using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;
using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Certification.Commands
{
	public class IssueCertificateCommand : ICommand<Guid>
	{
		public Guid StudentId { get; }
		public Guid CourseId { get; }
		public DateTime IssueDate { get; }
		public DateTime ExpirationDate { get; }

		public IssueCertificateCommand(Guid studentId, Guid courseId, DateTime issueDate, DateTime expirationDate)
		{
			StudentId = studentId;
			CourseId = courseId;
			IssueDate = issueDate;
			ExpirationDate = expirationDate;
		}
	}
}
