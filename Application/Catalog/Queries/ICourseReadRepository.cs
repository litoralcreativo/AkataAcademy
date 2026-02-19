using AkataAcademy.Application.Catalog.DTOs;

namespace AkataAcademy.Application.Catalog.Queries
{
	public interface ICourseReadRepository
	{
		CourseDto GetById(Guid id);
		IEnumerable<CourseDto> GetPublishedCourses();
	}
}