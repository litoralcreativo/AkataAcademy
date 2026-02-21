using AkataAcademy.Application.Certification.DTOs;
using AkataAcademy.Application.Common;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Certification.Queries
{
	public class GetValidCertificatesQueryHandler : IQueryHandler<GetValidCertificatesQuery, IEnumerable<CertificateDto>>
	{
		private readonly ICertificateReadRepository _certificateRepository;

		public GetValidCertificatesQueryHandler(ICertificateReadRepository certificateRepository)
		{
			_certificateRepository = certificateRepository;
		}

		public async Task<Result<IEnumerable<CertificateDto>>> Handle(GetValidCertificatesQuery query)
		{
			var certificates = await _certificateRepository.GetValidCertificates();

			if (certificates is null || !certificates.Any())
				return Result.Failure<IEnumerable<CertificateDto>>(Error.NotFound("Certification.NotFound", "No valid certificates found."));

			return Result.Success(certificates);
		}
	}
}
