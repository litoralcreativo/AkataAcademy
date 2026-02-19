using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Common;
using AkataAcademy.Domain.Common;

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

        public async Task<Result<CourseDto>> Handle(GetCourseByIdQuery query)
        {
            return await _readRepository.GetById(query.CourseId);
        }
    }
}