using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;

namespace AkataAcademy.UnitTests.Domain.Catalog
{
	public static class CatalogTestElements
	{
		public static readonly CourseTitle ValidTitle = CourseTitle.From("Valid title");
		public static readonly CourseDescription ValidDescription = CourseDescription.From("Valid description");
		public static readonly ModuleTitle ValidModuleTitle = ModuleTitle.From("Module");
		public static readonly ModuleTitle SecondValidModuleTitle = ModuleTitle.From("Module 2");
		public static readonly ModuleDuration ValidModuleDuration = ModuleDuration.From(60);
		public static readonly ModuleDuration SecondValidModuleDuration = ModuleDuration.From(80);
	}
}
