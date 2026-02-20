using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Common
{
	public abstract class DTO<TDomain, TId> where TDomain : Entity<TId>
	{
		public static Type DomainType => typeof(TDomain);
	}
}
