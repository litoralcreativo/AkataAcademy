using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Catalog.Commands
{
    public record CreateCourseCommand(string Title, string Description) : ICommand<Guid>;
}