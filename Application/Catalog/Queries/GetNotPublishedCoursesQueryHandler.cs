using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Common;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Catalog.Queries
{
    public class GetNotPublishedCoursesQueryHandler : IQueryHandler<GetNotPublishedCoursesQuery, IEnumerable<CourseDto>>
    {
        private readonly ICourseReadRepository _readRepository;

        public GetNotPublishedCoursesQueryHandler(
            ICourseReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<Result<IEnumerable<CourseDto>>> Handle(GetNotPublishedCoursesQuery query)
        {
            IEnumerable<CourseDto> courses = await _readRepository.GetNotPublishedCourses();

            if (courses is null)
                return Result.Success(Enumerable.Empty<CourseDto>());

            return Result.Success(courses);
        }
    }
}