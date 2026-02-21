using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Common
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<Result> Handle(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        Task<Result<TResult>> Handle(TCommand command);
    }
}