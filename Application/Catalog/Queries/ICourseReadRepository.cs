using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.Catalog.Entities;

namespace AkataAcademy.Application.Catalog.Queries
{
    public interface ICourseReadRepository : IReadRepository<Course, CourseDto, Guid>
    {
        Task<IEnumerable<CourseDto>> GetPublishedCourses();
        Task<IEnumerable<CourseDto>> GetNotPublishedCourses();
    }
}