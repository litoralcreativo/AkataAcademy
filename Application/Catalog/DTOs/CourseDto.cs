namespace AkataAcademy.Application.Catalog.DTOs
{
	public class CourseDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public bool IsPublished { get; set; }
		public List<CourseModuleDto> Modules { get; set; } = new List<CourseModuleDto>();
	}
}