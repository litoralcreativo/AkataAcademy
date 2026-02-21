using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Catalog.Repositories;
using AkataAcademy.Domain.Common;

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

        public async Task<Result> Handle(PublishCourseCommand command)
        {
            var course = await _repository.GetByIdAsync(command.CourseId);

            if (course is null)
            {
                return Result.Failure(Error.NotFound(ErrorCodes.Course.NotFound, $"Course with id {command.CourseId} not found."));
            }

            var result = course.Publish();

            if (result.IsFailure) return result;

            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}