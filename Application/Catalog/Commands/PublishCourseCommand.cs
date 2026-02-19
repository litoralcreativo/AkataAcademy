using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Catalog.Commands
{
	public class PublishCourseCommand : ICommand
	{
		public Guid CourseId { get; }

		public PublishCourseCommand(Guid courseId)
		{
			CourseId = courseId;
		}
	}
}