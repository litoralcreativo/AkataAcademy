using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Catalog.Entities
{
  public class CourseModule : Entity
  {
    public ModuleTitle Title { get; private set; }
    public Duration Duration { get; private set; }

    protected CourseModule()
    {
      Title = default!;
      Duration = default!;
    } // EF

    internal CourseModule(ModuleTitle title, Duration duration)
    {
      if (title == null) throw new DomainException("Title is required");
      if (duration == null) throw new DomainException("Duration is required");

      Id = Guid.NewGuid();
      Title = title;
      Duration = duration;
    }
  }
}
