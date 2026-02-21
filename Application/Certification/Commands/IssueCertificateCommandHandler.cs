using AkataAcademy.Application.Catalog.Queries;
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
		private readonly ICourseReadRepository _courseReadRepository;

		public IssueCertificateCommandHandler(
			ICertificateRepository certificateRepository,
			CertificateDomainService domainService,
			IUnitOfWork unitOfWork,
			ICourseReadRepository courseReadRepository)
		{
			_certificateRepository = certificateRepository;
			_domainService = domainService;
			_unitOfWork = unitOfWork;
			_courseReadRepository = courseReadRepository;
		}

		public async Task<Result<Guid>> Handle(IssueCertificateCommand command)
		{
			StudentId studentId = new StudentId(command.StudentId);
			CourseId courseId = new CourseId(command.CourseId);
			IssueDate issueDate = new IssueDate(command.IssueDate);
			ExpirationDate expirationDate = new ExpirationDate(command.ExpirationDate);

			// Check if course exists
			var course = await _courseReadRepository.GetById(courseId.Value);
			if (course is null)
				return Result.Failure<Guid>(Error.NotFound(ErrorCodes.Course.NotFound, $"Course with id {courseId.Value} not found."));

			var existingCertificate = await _certificateRepository.GetByStudentAndCourseAsync(studentId, courseId);

			Result canIssue = _domainService.CanIssueCertificate(
				existingCertificate,
				issueDate,
				expirationDate);

			if (!canIssue.IsSuccess)
				return Result.Failure<Guid>(canIssue.Error);

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
