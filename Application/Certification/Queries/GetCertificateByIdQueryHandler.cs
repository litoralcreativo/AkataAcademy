using AkataAcademy.Application.Certification.DTOs;
using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Certification.Repositories;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Certification.Queries
{
	public class GetCertificateByIdQueryHandler : IQueryHandler<GetCertificateByIdQuery, CertificateDto>
	{
		private readonly ICertificateReadRepository _certificateRepository;

		public GetCertificateByIdQueryHandler(ICertificateReadRepository certificateRepository)
		{
			_certificateRepository = certificateRepository;
		}

		public async Task<Result<CertificateDto>> Handle(GetCertificateByIdQuery query)
		{
			var certificate = await _certificateRepository.GetById(query.CertificateId);

			if (certificate is null)
				return Result.Failure<CertificateDto>(Error.NotFound(ErrorCodes.Certificate.NotFound, $@"Certificate with id {query.CertificateId} not found."));

			return Result.Success(certificate);
		}
	}
}
