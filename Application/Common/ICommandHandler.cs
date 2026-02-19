namespace AkataAcademy.Application.Common
{
	public interface ICommandHandler<TCommand> where TCommand : ICommand
	{
		void Handle(TCommand command);
	}

	public interface ICommandHandler<TCommand, TResult>
	where TCommand : ICommand<TResult>
	{
		TResult Handle(TCommand command);
	}
}