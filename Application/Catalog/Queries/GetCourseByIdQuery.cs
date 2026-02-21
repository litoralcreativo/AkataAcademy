using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Catalog.Queries
{
    public record GetCourseByIdQuery(Guid CourseId) : IQuery<CourseDto>;
}