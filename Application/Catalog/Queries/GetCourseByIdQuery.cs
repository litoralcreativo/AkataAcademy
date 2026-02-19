using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Catalog.Queries
{
    public class GetCourseByIdQuery : IQuery<CourseDto>
    {
        public Guid CourseId { get; }

        public GetCourseByIdQuery(Guid courseId)
        {
            CourseId = courseId;
        }
    }
}