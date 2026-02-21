using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Catalog.Commands
{
    public record PublishCourseCommand(Guid CourseId) : ICommand
    {
    }
}