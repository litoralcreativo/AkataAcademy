using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Common;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.Catalog.Queries
{
    public class GetPublishedCoursesQueryHandler : IQueryHandler<GetPublishedCoursesQuery, IEnumerable<CourseDto>>
    {
        private readonly ICourseReadRepository _readRepository;

        public GetPublishedCoursesQueryHandler(
            ICourseReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<Result<IEnumerable<CourseDto>>> Handle(GetPublishedCoursesQuery query)
        {
            IEnumerable<CourseDto> courses = await _readRepository.GetPublishedCourses();

            if (courses is null)
                return Result.Success(Enumerable.Empty<CourseDto>());

            return Result.Success(courses);
        }
    }
}