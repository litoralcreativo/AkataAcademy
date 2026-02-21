using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Catalog.Repositories;
using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;
using AkataAcademy.Domain.Common;
using System.Threading.Tasks;

namespace AkataAcademy.Application.Catalog.Commands
{
	public class AddModuleToCourseCommandHandler : ICommandHandler<AddModuleToCourseCommand, Guid>
	{
		private readonly ICourseRepository _repository;
		private readonly IUnitOfWork _unitOfWork;

		public AddModuleToCourseCommandHandler(
			ICourseRepository repository,
			IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<Guid>> Handle(AddModuleToCourseCommand command)
		{
			var course = await _repository.GetByIdAsync(command.CourseId);
			if (course is null)
				return Result.Failure<Guid>(Error.NotFound("Course", $"Course with id {command.CourseId} not found."));

			var title = ModuleTitle.From(command.ModuleTitle);
			var duration = ModuleDuration.From(command.ModuleDuration);


			var result = course.AddModule(title, duration);
			if (result.IsFailure)
				return Result.Failure<Guid>(result.Error);

			await _repository.AddCourseModule(course.Modules.Last());

			await _unitOfWork.SaveChangesAsync();

			return Result.Success(result.Value.Id);
		}
	}
}
