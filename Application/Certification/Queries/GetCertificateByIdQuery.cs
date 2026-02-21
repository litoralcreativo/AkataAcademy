using AkataAcademy.Application.Certification.DTOs;
using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Certification.Queries
{
	public record GetCertificateByIdQuery(Guid CertificateId) : IQuery<CertificateDto>;
}