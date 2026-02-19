using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Common
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<Result<TResult>> Handle(TQuery query);
    }
}