using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;
using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Certification.Commands
{
	public record IssueCertificateCommand(Guid StudentId, Guid CourseId, DateTime IssueDate, DateTime ExpirationDate) : ICommand<Guid>;
}