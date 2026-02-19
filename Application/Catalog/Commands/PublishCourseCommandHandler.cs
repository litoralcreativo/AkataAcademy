using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Catalog.Repositories;

namespace AkataAcademy.Application.Catalog.Commands
{
    public class PublishCourseCommandHandler : ICommandHandler<PublishCourseCommand>
    {
        private readonly ICourseRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PublishCourseCommandHandler(
            ICourseRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async void Handle(PublishCourseCommand command)
        {
            var course = await _repository.GetByIdAsync(command.CourseId);

            if (course is null)
            {
                return;
            }

            course.Publish();

            await _unitOfWork.SaveChangesAsync();
        }
    }
}