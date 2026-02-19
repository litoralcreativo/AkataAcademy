using AkataAcademy.Application.Catalog.DTOs;

namespace AkataAcademy.Application.Catalog.Queries
{
	public interface ICourseReadRepository
	{
		Task<CourseDto?> GetById(Guid id);
		Task<IEnumerable<CourseDto>> GetPublishedCourses();
	}
}