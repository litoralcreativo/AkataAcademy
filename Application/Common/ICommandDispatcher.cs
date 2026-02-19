namespace AkataAcademy.Application.Common
{
	public interface ICommandDispatcher
	{
		void Dispatch(ICommand command);
		TResult Dispatch<TResult>(ICommand<TResult> command);
	}
}