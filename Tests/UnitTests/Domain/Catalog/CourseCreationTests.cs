using AkataAcademy.Domain.BoundedContexts.Catalog.Entities;
using AkataAcademy.Domain.BoundedContexts.Catalog.Events;
using AkataAcademy.Domain.BoundedContexts.Catalog.ValueObjects;
using AkataAcademy.Domain.Common;
using AkataAcademy.UnitTests.Common;
using static AkataAcademy.UnitTests.Domain.Catalog.CatalogTestElements;

namespace AkataAcademy.UnitTests.Domain.Catalog
{
	public class CourseCreationTests
	{
		[Fact]
		public void CourseDescription_FromEmpty_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => CourseDescription.From(""));
		}

		[Fact]
		public void CourseTitle_FromEmpty_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => CourseTitle.From(""));
		}

		[Fact]
		public void Create_WithNullValues_ShouldFail()
		{
			var withNullTitle = Course.Create(null, ValidDescription);
			Assert.True(withNullTitle.IsFailure);

			var withNullDescription = Course.Create(ValidTitle, null);
			Assert.True(withNullDescription.IsFailure);
		}

		[Theory]
		[InlineData(5)]
		[InlineData(10)]
		[InlineData(20)]
		public void Create_WithRandomTitle_ShouldSucceed(int titleLength)
		{
			var randomTitle = CourseTitle.From(TestDataGenerator.RandomString(titleLength));
			var randomDescription = CourseDescription.From(TestDataGenerator.RandomString(30));
			var result = Course.Create(randomTitle, randomDescription);
			Assert.True(result.IsSuccess);
			Assert.NotNull(result.Value);
		}

		[Theory]
		[MemberData(nameof(ValidCourseData), MemberType = typeof(CatalogTestElements))]
		public void Create_WithValidData_ShouldSucceed(CourseTitle title, CourseDescription description)
		{
			var result = Course.Create(title, description);
			Assert.True(result.IsSuccess);
			Assert.NotNull(result.Value);
		}

		[Theory]
		[MemberData(nameof(ValidCourseModuleData), MemberType = typeof(CatalogTestElements))]
		public void CourseCreated_EventIsEmittedOnCreation(CourseTitle title, CourseDescription description, ModuleTitle moduleTitle, ModuleDuration moduleDuration)
		{
			var result = Course.Create(title, description);
			Assert.True(result.IsSuccess);
			var course = result.Value;
			Assert.Contains(course.DomainEvents, e => e is CourseCreated);
		}

		[Theory]
		[MemberData(nameof(ValidCourseModuleData), MemberType = typeof(CatalogTestElements))]
		public void CourseCreated_EventIsEmittedOnCreation_WithCorrectData(CourseTitle title, CourseDescription description, ModuleTitle moduleTitle, ModuleDuration moduleDuration)
		{
			var result = Course.Create(title, description);
			Assert.True(result.IsSuccess);
			var course = result.Value;
			var domainEvent = course.DomainEvents.OfType<CourseCreated>().FirstOrDefault();
			Assert.NotNull(domainEvent);
			Assert.Equal(course.Id, domainEvent.CourseId);
		}

		[Theory]
		[MemberData(nameof(ValidCourseModuleData), MemberType = typeof(CatalogTestElements))]
		public void AddModule_ToCourse_ShouldSucceed(CourseTitle title, CourseDescription description, ModuleTitle moduleTitle, ModuleDuration moduleDuration)
		{
			var courseResult = Course.Create(title, description);
			Assert.True(courseResult.IsSuccess);
			var course = courseResult.Value;
			var addModuleResult = course.AddModule(moduleTitle, moduleDuration);
			Assert.True(addModuleResult.IsSuccess);
			Assert.Contains(course.Modules, m => m.Title == moduleTitle && m.Duration == moduleDuration);
		}
	}
}
