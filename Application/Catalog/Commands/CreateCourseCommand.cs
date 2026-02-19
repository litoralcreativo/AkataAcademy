using AkataAcademy.Application.Common;

namespace AkataAcademy.Application.Catalog.Commands
{
    public class CreateCourseCommand : ICommand<Guid>
    {
        public string Title { get; }
        public string Description { get; }

        public CreateCourseCommand(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}