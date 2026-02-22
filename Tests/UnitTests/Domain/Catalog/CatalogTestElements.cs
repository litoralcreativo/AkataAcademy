using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;
using System.Collections.Generic;
using System.Linq;

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

		public static readonly CourseTitle[] ValidTitles = new[]
		{
			CourseTitle.From("Valid title"),
			CourseTitle.From("Advanced course"),
			CourseTitle.From("Beginner course"),
			CourseTitle.From("Intermediate course"),
			CourseTitle.From("Expert course")
		};

		public static readonly CourseDescription[] ValidDescriptions = new[]
		{
			CourseDescription.From("Valid description"),
			CourseDescription.From("Learn the basics"),
			CourseDescription.From("Deep dive into topics"),
			CourseDescription.From("Step-by-step guide"),
			CourseDescription.From("Comprehensive curriculum")
		};

		public static readonly ModuleTitle[] ValidModuleTitles = new[]
		{
			ModuleTitle.From("Module"),
			ModuleTitle.From("Module 2"),
			ModuleTitle.From("Module 3"),
			ModuleTitle.From("Module 4"),
			ModuleTitle.From("Module 5")
		};

		public static readonly ModuleDuration[] ValidModuleDurations = new[]
		{
			ModuleDuration.From(60),
			ModuleDuration.From(80),
			ModuleDuration.From(45),
			ModuleDuration.From(90),
			ModuleDuration.From(120)
		};

		public static IEnumerable<object[]> ValidTitlesData => ValidTitles.Select(t => new object[] { t });
		public static IEnumerable<object[]> ValidDescriptionsData => ValidDescriptions.Select(d => new object[] { d });
		public static IEnumerable<object[]> ValidModuleTitlesData => ValidModuleTitles.Select(mt => new object[] { mt });
		public static IEnumerable<object[]> ValidModuleDurationsData => ValidModuleDurations.Select(md => new object[] { md });

		public static IEnumerable<object[]> ValidCourseModuleData =>
			ValidTitles.Zip(ValidDescriptions, (title, desc) => new { title, desc })
			.Zip(ValidModuleTitles, (pair, mt) => new { pair, mt })
			.Zip(ValidModuleDurations, (trio, md) => new object[] { trio.pair.title, trio.pair.desc, trio.mt, md });

		public static IEnumerable<object[]> ValidCourseData =>
			ValidTitles.Zip(ValidDescriptions, (title, desc) => new object[] { title, desc });
	}
}
