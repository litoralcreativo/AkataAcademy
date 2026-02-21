using System.Reflection;
using AkataAcademy.Domain.BoundedContexts.Catalog.Events;
using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.Entities
{
    public class Course : AggregateRoot
    {
        public CourseTitle Title { get; private set; } = null!;
        public CourseDescription Description { get; private set; } = null!;
        public bool IsPublished { get; private set; }

        public virtual ICollection<CourseModule> Modules { get; private set; } = new List<CourseModule>();
        protected Course() // EF
        {
        }

        private Course(CourseTitle title, CourseDescription description)
        {
            if (title == null) throw new DomainException("Title is required");
            if (description == null) throw new DomainException("Description is required");

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            IsPublished = false;

            AddDomainEvent(new CourseCreated(Id));
        }

        public static Result<Course> Create(
            CourseTitle title,
            CourseDescription description)
        {
            try
            {
                return new Course(title, description);
            }
            catch (DomainException ex)
            {
                return Result.Failure<Course>(Error.Validation(ErrorCodes.Course.Creation, ex.Message));
            }
            catch (Exception)
            {
                return Result.Failure<Course>(Error.Failure(ErrorCodes.General.Conflict, "An unexpected error occurred while creating the course."));
            }
        }

        public Result<CourseModule> AddModule(ModuleTitle title, ModuleDuration duration)
        {
            if (IsPublished)
                return Result.Failure<CourseModule>(Error.Validation(ErrorCodes.Course.ModuleManagment, "No modules can be added to a published course"));

            if (Modules.Any(m => m.Title.Equals(title)))
                return Result.Failure<CourseModule>(Error.Validation(ErrorCodes.Course.ModuleManagment, "A module with that title already exists in this course"));

            try
            {
                var module = new CourseModule(Id, title, duration);
                Modules.Add(module);


                AddDomainEvent(new ModuleAddedToCourse(Id, module.Id));
                return Result.Success(module);
            }
            catch (DomainException ex)
            {
                return Result.Failure<CourseModule>(Error.Validation(ErrorCodes.Course.ModuleManagment, ex.Message));
            }
            catch (Exception)
            {
                return Result.Failure<CourseModule>(Error.Failure(ErrorCodes.General.Conflict, "An unexpected error occurred while adding the module."));
            }

        }

        public Result Publish()
        {
            if (IsPublished)
                return Result.Failure(Error.Validation(ErrorCodes.Course.Publishing, "The course is already published"));

            if (!Modules.Any())
                return Result.Failure(Error.Validation(ErrorCodes.Course.Publishing, "A course cannot be published without modules"));

            IsPublished = true;

            AddDomainEvent(new CoursePublished(Id));
            return Result.Success();
        }
    }
}