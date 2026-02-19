using System.Threading.Tasks;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Common
{
    public interface ICommandDispatcher
    {
        Task Dispatch(ICommand command);
        Task<Result<TResult>> Dispatch<TResult>(ICommand<TResult> command);
    }
}