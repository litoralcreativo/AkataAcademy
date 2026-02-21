using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Certification.DTOs;
using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Catalog.Entities;
using AkataAcademy.Domain.BoundedContexts.Certification.Entities;

namespace AkataAcademy.Application.Certification.Queries
{
	public interface ICertificateReadRepository : IReadRepository<Certificate, CertificateDto, Guid>
	{
		Task<IEnumerable<CertificateDto>> GetValidCertificates();
	}
}