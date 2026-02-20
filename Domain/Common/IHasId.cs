namespace AkataAcademy.Domain.Common
{
	public interface IHasId<TId>
	{
		TId Id { get; }
	}
}
