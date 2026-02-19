using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Common;

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

		public IEnumerable<CourseDto> Handle(GetPublishedCoursesQuery query)
		{
			return _readRepository.GetPublishedCourses();
		}
	}
}