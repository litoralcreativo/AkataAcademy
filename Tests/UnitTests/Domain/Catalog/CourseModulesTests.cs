using AkataAcademy.Domain.BoundedContexts.Catalog.Entities;
using AkataAcademy.Domain.BoundedContexts.Catalog.Events;
using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;
using AkataAcademy.Domain.Common;
using AkataAcademy.UnitTests.Common;
using static AkataAcademy.UnitTests.Domain.Catalog.CatalogTestElements;

namespace AkataAcademy.UnitTests.Domain.Catalog
{
	public class CourseModulesTests
	{
		[Fact]
		public void ModuleTitle_FromEmpty_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => ModuleTitle.From(""));
		}

		[Fact]
		public void ModuleDuration_FromInvalidValue_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => ModuleDuration.From(0));
			Assert.Throws<DomainException>(() => ModuleDuration.From(-10));
		}

		[Fact]
		public void Module_WithNullValues_ShouldFail()
		{
			var course = Course.Create(ValidTitle, ValidDescription).Value;
			var withNullTitle = course.AddModule(null, ValidModuleDuration);
			Assert.True(withNullTitle.IsFailure);
			var withNullDuration = course.AddModule(ValidModuleTitle, null);
			Assert.True(withNullDuration.IsFailure);
		}

		[Fact]
		public void AddModule_ToPublishedCourse_ShouldFail()
		{
			var course = Course.Create(ValidTitle, ValidDescription).Value;
			course.AddModule(ValidModuleTitle, ValidModuleDuration);
			var publishResult = course.Publish();
			Assert.True(publishResult.IsSuccess);
			Assert.True(course.IsPublished);
			var result = course.AddModule(SecondValidModuleTitle, SecondValidModuleDuration);
			Assert.True(result.IsFailure);
		}

		[Theory]
		[InlineData(3)]
		[InlineData(5)]
		public void AddModule_WithRandomData_ShouldSucceed(int moduleCount)
		{
			var course = Course.Create(ValidTitle, ValidDescription).Value;
			for (int i = 0; i < moduleCount; i++)
			{
				var moduleTitle = ModuleTitle.From(TestDataGenerator.RandomString(8));
				var moduleDuration = ModuleDuration.From(TestDataGenerator.RandomDuration());
				var result = course.AddModule(moduleTitle, moduleDuration);
				Assert.True(result.IsSuccess);
			}
		}

		[Fact]
		public void AddModule_WithDuplicateTitle_ShouldFail()
		{
			var course = Course.Create(ValidTitle, ValidDescription).Value;
			var result1 = course.AddModule(ValidModuleTitle, ValidModuleDuration);
			Assert.True(result1.IsSuccess);
			var result2 = course.AddModule(ValidModuleTitle, SecondValidModuleDuration);
			Assert.True(result2.IsFailure);
		}

		[Fact]
		public void GetModules_ReturnsAllAddedModules()
		{
			var course = Course.Create(ValidTitle, ValidDescription).Value;
			var result1 = course.AddModule(ValidModuleTitle, ValidModuleDuration);
			var result2 = course.AddModule(SecondValidModuleTitle, SecondValidModuleDuration);
			Assert.True(result1.IsSuccess);
			Assert.True(result2.IsSuccess);
			var modules = course.Modules.ToList();
			Assert.Equal(2, modules.Count);
			Assert.Contains(modules, m => m.Title == ValidModuleTitle);
			Assert.Contains(modules, m => m.Title == SecondValidModuleTitle);
		}

		[Fact]
		public void ModuleAddedToCourse_EventIsEmittedOnAddModule()
		{
			var course = Course.Create(ValidTitle, ValidDescription).Value;
			var addResult = course.AddModule(ValidModuleTitle, ValidModuleDuration);
			Assert.True(addResult.IsSuccess);
			Assert.Contains(course.DomainEvents, e => e is ModuleAddedToCourse);
		}

		[Fact]
		public void ModuleAddedToCourse_EventIsNotEmittedOnDuplicateTitle()
		{
			var course = Course.Create(ValidTitle, ValidDescription).Value;
			var result1 = course.AddModule(ValidModuleTitle, ValidModuleDuration);
			Assert.True(result1.IsSuccess);
			var result2 = course.AddModule(ValidModuleTitle, SecondValidModuleDuration);
			Assert.True(result2.IsFailure);
			Assert.Single(course.DomainEvents, e => e is ModuleAddedToCourse);
		}
	}
}
