using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Common
{
	public interface IReadRepository<TDom, TDto, TId>
	where TDom : Entity<TId>
	where TDto : DTO<TDom, TId>
	{
		Task<TDto?> GetById(TId id);
	}
}