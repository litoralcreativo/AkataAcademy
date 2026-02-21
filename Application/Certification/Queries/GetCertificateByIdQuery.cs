using AkataAcademy.Application.Certification.DTOs;
using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Certification.Queries
{
	public class GetCertificateByIdQuery : IQuery<CertificateDto>
	{
		public Guid CertificateId { get; }

		public GetCertificateByIdQuery(Guid certificateId)
		{
			CertificateId = certificateId;
		}
	}
}
