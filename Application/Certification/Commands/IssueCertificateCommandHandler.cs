using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Certification.Entities;
using AkataAcademy.Domain.BoundedContexts.Certification.Repositories;
using AkataAcademy.Domain.BoundedContexts.Certification.Services;
using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Certification.Commands
{
	public class IssueCertificateCommandHandler : ICommandHandler<IssueCertificateCommand, Guid>
	{
		private readonly ICertificateRepository _certificateRepository;
		private readonly CertificateDomainService _domainService;
		private readonly IUnitOfWork _unitOfWork;

		public IssueCertificateCommandHandler(
			ICertificateRepository certificateRepository,
			CertificateDomainService domainService,
			IUnitOfWork unitOfWork)
		{
			_certificateRepository = certificateRepository;
			_domainService = domainService;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<Guid>> Handle(IssueCertificateCommand command)
		{
			StudentId studentId = new StudentId(command.StudentId);
			CourseId courseId = new CourseId(command.CourseId);
			IssueDate issueDate = new IssueDate(command.IssueDate);
			ExpirationDate expirationDate = new ExpirationDate(command.ExpirationDate);

			var existingCertificate = await _certificateRepository.GetByStudentAndCourseAsync(studentId, courseId);

			bool canIssue = _domainService.CanIssueCertificate(
				existingCertificate,
				issueDate,
				expirationDate);

			if (!canIssue)
				throw new DomainException("Certificate cannot be issued due to business rules.");

			var certificate = Certificate.Issue(
				studentId,
				courseId,
				issueDate,
				expirationDate);

			await _certificateRepository.AddAsync(certificate);

			await _unitOfWork.SaveChangesAsync();

			return certificate.Id;
		}
	}
}
