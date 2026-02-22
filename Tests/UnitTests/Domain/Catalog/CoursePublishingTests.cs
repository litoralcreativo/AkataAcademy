using AkataAcademy.Domain.BoundedContexts.Catalog.Entities;
using AkataAcademy.Domain.BoundedContexts.Catalog.Events;
using static AkataAcademy.UnitTests.Domain.Catalog.CatalogTestElements;

namespace AkataAcademy.UnitTests.Domain.Catalog
{
	public class CoursePublishingTests
	{
		[Fact]
		public void Publish_WithoutModules_ShouldFail()
		{
			var course = Course.Create(ValidTitle, ValidDescription).Value;
			var result = course.Publish();
			Assert.True(result.IsFailure);
		}

		[Fact]
		public void Publish_WithModules_ShouldSucceed()
		{
			var course = Course.Create(ValidTitle, ValidDescription).Value;
			course.AddModule(ValidModuleTitle, ValidModuleDuration);
			var result = course.Publish();
			Assert.True(result.IsSuccess);
			Assert.True(course.IsPublished);
		}

		[Fact]
		public void Publish_AlreadyPublishedCourse_ShouldFail()
		{
			var course = Course.Create(ValidTitle, ValidDescription).Value;
			course.AddModule(ValidModuleTitle, ValidModuleDuration);
			var firstPublish = course.Publish();
			Assert.True(firstPublish.IsSuccess);
			Assert.True(course.IsPublished);
			var secondPublish = course.Publish();
			Assert.True(secondPublish.IsFailure);
		}

		[Fact]
		public void CoursePublished_EventIsEmittedOnPublish()
		{
			var course = Course.Create(ValidTitle, ValidDescription).Value;
			course.AddModule(ValidModuleTitle, ValidModuleDuration);
			var publishResult = course.Publish();
			Assert.True(publishResult.IsSuccess);
			Assert.Contains(course.DomainEvents, e => e is CoursePublished);
		}

		[Fact]
		public void CoursePublished_EventIsNotEmittedOnPublishWithoutModules()
		{
			var course = Course.Create(ValidTitle, ValidDescription).Value;
			var publishResult = course.Publish();
			Assert.True(publishResult.IsFailure);
			Assert.DoesNotContain(course.DomainEvents, e => e is CoursePublished);
		}
	}
}
