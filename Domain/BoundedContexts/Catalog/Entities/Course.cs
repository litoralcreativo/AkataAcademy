using AkataAcademy.Domain.BoundedContexts.Catalog.Events;
using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.Entities
{
    public class Course : AggregateRoot
    {
        private readonly List<CourseModule> _modules;

        public CourseTitle Title { get; private set; } = null!;
        public CourseDescription Description { get; private set; } = null!;
        public bool IsPublished { get; private set; }

        public virtual ICollection<CourseModule> Modules => _modules;

        protected Course() // EF
        {
            _modules = new List<CourseModule>();
        }

        private Course(CourseTitle title, CourseDescription description)
        {
            if (title == null) throw new DomainException("Title is required");
            if (description == null) throw new DomainException("Description is required");

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            IsPublished = false;

            _modules = new List<CourseModule>();

            AddDomainEvent(new CourseCreated(Id));
        }

        public static Course Create(
            CourseTitle title,
            CourseDescription description)
        {
            return new Course(title, description);
        }

        public void AddModule(ModuleTitle title, Duration duration)
        {
            if (IsPublished)
                throw new DomainException("No modules can be added to a published course");

            if (_modules.Any(m => m.Title.Equals(title)))
                throw new DomainException("A module with that title already exists in this course");

            var module = new CourseModule(Id, title, duration);

            _modules.Add(module);
            // Sincroniza la colección pública
            // (no es necesario si Modules solo expone _modules)

            AddDomainEvent(new ModuleAddedToCourse(Id, module.Id));
        }

        public void Publish()
        {
            if (IsPublished)
                throw new DomainException("The course is already published");

            if (!_modules.Any())
                throw new DomainException("A course cannot be published without modules");

            IsPublished = true;

            AddDomainEvent(new CoursePublished(Id));
        }
    }
}