using AkataAcademy.Application.Common;
using AkataAcademy.Domain.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AkataAcademy.Application.Dispatchers
{
	public class CommandDispatcher : ICommandDispatcher
	{
		private readonly IServiceProvider _serviceProvider;

		public CommandDispatcher(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task Dispatch(ICommand command)
		{
			var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
			dynamic handler = _serviceProvider.GetRequiredService(handlerType);
			await handler.Handle((dynamic)command);
		}

		public async Task<Result<TResult>> Dispatch<TResult>(ICommand<TResult> command)
		{
			try
			{
				var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
				dynamic handler = _serviceProvider.GetRequiredService(handlerType);
				return await handler.Handle((dynamic)command);
			}
			catch (Exception ex)
			{
				return Result.Failure<TResult>(Error.Failure("CommandDispatcher", ex.Message));
			}
		}
	}
}