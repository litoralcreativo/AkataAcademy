using AkataAcademy.Application.Common;
using AkataAcademy.Domain.Common;
using Microsoft.Extensions.DependencyInjection;

namespace AkataAcademy.Application.Dispatchers
{
	public class QueryDispatcher : IQueryDispatcher
	{
		private readonly IServiceProvider _serviceProvider;

		public QueryDispatcher(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task<Result<TResult>> Dispatch<TResult>(IQuery<TResult> query)
		{
			var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
			dynamic handler = _serviceProvider.GetRequiredService(handlerType);
			return await handler.Handle((dynamic)query);
		}
	}
}