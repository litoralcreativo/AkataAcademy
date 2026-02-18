using Domain.BoundedContexts.Catalog.Events;
using Domain.BoundedContexts.Catalog.ValueObjects;
using Domain.Common;

namespace Domain.BoundedContexts.Catalog.Entities
{
    public class Course : AggregateRoot
    {
        private readonly List<CourseModule> _modules;

        public CourseTitle Title { get; private set; } = null!;
        public CourseDescription Description { get; private set; } = null!;
        public bool IsPublished { get; private set; }

        public virtual ICollection<CourseModule> Modules { get; protected set; } = new List<CourseModule>();

        protected Course() // EF
        {
            _modules = new List<CourseModule>();
        }

        private Course(CourseTitle title, CourseDescription description)
        {
            if (title == null) throw new DomainException("Title requerido");
            if (description == null) throw new DomainException("Description requerida");

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
                throw new DomainException("No se pueden agregar módulos a un curso publicado");

            if (_modules.Any(m => m.Title.Equals(title)))
                throw new DomainException("Ya existe un módulo con ese título");

            var module = new CourseModule(title, duration);

            _modules.Add(module);

            AddDomainEvent(new ModuleAddedToCourse(Id, module.Id));
        }

        public void Publish()
        {
            if (IsPublished)
                throw new DomainException("El curso ya está publicado");

            if (!_modules.Any())
                throw new DomainException("No se puede publicar un curso sin módulos");

            IsPublished = true;

            AddDomainEvent(new CoursePublished(Id));
        }
    }
}