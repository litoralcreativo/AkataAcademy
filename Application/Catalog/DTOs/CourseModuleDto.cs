namespace AkataAcademy.Application.Catalog.DTOs
{
	public class CourseModuleDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public int DurationMinutes { get; set; }
	}
}