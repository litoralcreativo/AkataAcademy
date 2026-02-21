using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Catalog.Entities;
using AkataAcademy.Domain.BoundedContexts.Catalog.Repositories;
using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Catalog.Commands
{
    public class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, Guid>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCourseCommandHandler(
            ICourseRepository repository,
            IUnitOfWork unitOfWork)
        {
            _courseRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateCourseCommand command)
        {
            var course = Course.Create(
                CourseTitle.From(command.Title),
                CourseDescription.From(command.Description));

            if (course.IsFailure)
                return Result.Failure<Guid>(course.Error);

            await _courseRepository.AddAsync(course.Value);

            await _unitOfWork.SaveChangesAsync();

            return course.Value.Id;
        }
    }
}