namespace AkataAcademy.Application.Common
{
	public interface IQueryDispatcher
	{
		TResult Dispatch<TResult>(IQuery<TResult> query);
	}
}