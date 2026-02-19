using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Catalog.Queries
{
	public class GetCourseByIdQueryHandler : IQueryHandler<GetCourseByIdQuery, CourseDto>
	{
		private readonly ICourseReadRepository _readRepository;

		public GetCourseByIdQueryHandler(
			ICourseReadRepository readRepository)
		{
			_readRepository = readRepository;
		}

		public CourseDto Handle(GetCourseByIdQuery query)
		{
			return _readRepository.GetById(query.CourseId);
		}
	}
}