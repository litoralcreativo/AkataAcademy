using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.Entities
{
  public class CourseModule : Entity
  {
    public Guid CourseId { get; private set; }
    public ModuleTitle Title { get; private set; } = null!;
    public ModuleDuration Duration { get; private set; } = null!;

    protected CourseModule()
    {
      Title = default!;
      Duration = default!;
      CourseId = default!;
    } // EF

    internal CourseModule(Guid courseId, ModuleTitle title, ModuleDuration duration)
    {
      if (title == null) throw new DomainException("Title is required");
      if (duration == null) throw new DomainException("Duration is required");

      Id = Guid.NewGuid();
      CourseId = courseId;
      Title = title;
      Duration = duration;
    }
  }
}
