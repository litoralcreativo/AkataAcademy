using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Catalog.Entities;
using AkataAcademy.Domain.BoundedContexts.Catalog.Repositories;
using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Catalog.Commands
{
    public class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, Guid>
    {
        private readonly ICourseRepository __courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCourseCommandHandler(
            ICourseRepository repository,
            IUnitOfWork unitOfWork)
        {
            __courseRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateCourseCommand command)
        {
            var course = Course.Create(
                new CourseTitle(command.Title),
                new CourseDescription(command.Description));

            await __courseRepository.AddAsync(course);

            await _unitOfWork.SaveChangesAsync();

            return course.Id;
        }
    }
}