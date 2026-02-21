using AkataAcademy.Application.Certification.DTOs;
using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Certification.Queries
{
	public record GetValidCertificatesQuery : IQuery<IEnumerable<CertificateDto>>;
}
