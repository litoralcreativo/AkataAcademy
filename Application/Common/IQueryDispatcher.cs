using AkataAcademy.Domain.Common;
using System.Threading.Tasks;

namespace AkataAcademy.Application.Common
{
    public interface IQueryDispatcher
    {
        Task<Result<TResult>> Dispatch<TResult>(IQuery<TResult> query);
    }
}