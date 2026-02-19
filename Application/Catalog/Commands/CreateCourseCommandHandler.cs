using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Catalog.Entities;
using AkataAcademy.Domain.BoundedContexts.Catalog.Repositories;
using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;

namespace AkataAcademy.Application.Catalog.Commands
{
	public class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, Guid>
	{
		private readonly ICourseRepository _repository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateCourseCommandHandler(
			ICourseRepository repository,
			IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public Guid Handle(CreateCourseCommand command)
		{
			var course = Course.Create(
				new CourseTitle(command.Title),
				new CourseDescription(command.Description));

			_repository.Add(course);

			_unitOfWork.Commit();

			return course.Id;
		}
	}
}