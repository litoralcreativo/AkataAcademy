using AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects;
using AkataAcademy.Domain.Common;
using Xunit;

namespace AkataAcademy.UnitTests.Domain.Enrollment
{
	/// <summary>
	/// Test suite for ProgressIncrement Value Object
	/// Tests factory methods, validations, and calculations
	/// </summary>
	public class ProgressIncrementTests
	{
		[Fact]
		public void ProgressIncrement_Standard_ShouldReturn10Percent()
		{
			var increment = ProgressIncrement.Standard();

			Assert.NotNull(increment);
			Assert.Equal(10, increment.Value);
		}

		[Fact]
		public void ProgressIncrement_Fast_ShouldReturn5Percent()
		{
			var increment = ProgressIncrement.Fast();

			Assert.NotNull(increment);
			Assert.Equal(5, increment.Value);
		}

		[Fact]
		public void ProgressIncrement_Slow_ShouldReturn20Percent()
		{
			var increment = ProgressIncrement.Slow();

			Assert.NotNull(increment);
			Assert.Equal(20, increment.Value);
		}

		[Fact]
		public void ProgressIncrement_Custom_WithValidValue_ShouldSucceed()
		{
			var increment = ProgressIncrement.Custom(15);

			Assert.NotNull(increment);
			Assert.Equal(15, increment.Value);
		}

		[Fact]
		public void ProgressIncrement_Custom_WithMinValue_ShouldFail()
		{
			Assert.Throws<DomainException>(() => ProgressIncrement.Custom(0));
		}

		[Fact]
		public void ProgressIncrement_Custom_WithNegativeValue_ShouldFail()
		{
			Assert.Throws<DomainException>(() => ProgressIncrement.Custom(-5));
		}

		[Fact]
		public void ProgressIncrement_Custom_WithMaxValue_ShouldSucceed()
		{
			var increment = ProgressIncrement.Custom(100);

			Assert.NotNull(increment);
			Assert.Equal(100, increment.Value);
		}

		[Fact]
		public void ProgressIncrement_Custom_WithValueGreaterThan100_ShouldFail()
		{
			Assert.Throws<DomainException>(() => ProgressIncrement.Custom(101));
		}

		[Fact]
		public void ProgressIncrement_From_WithValidValue_ShouldSucceed()
		{
			var increment = ProgressIncrement.From(25);

			Assert.NotNull(increment);
			Assert.Equal(25, increment.Value);
		}

		[Fact]
		public void ProgressIncrement_From_WithInvalidValue_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => ProgressIncrement.From(0));
			Assert.Throws<DomainException>(() => ProgressIncrement.From(101));
		}

		[Fact]
		public void ProgressIncrement_GetLessonsNeeded_Standard_ShouldReturn10()
		{
			var increment = ProgressIncrement.Standard();

			var lessons = increment.GetLessonsNeeded();

			Assert.Equal(10, lessons);
		}

		[Fact]
		public void ProgressIncrement_GetLessonsNeeded_Fast_ShouldReturn20()
		{
			var increment = ProgressIncrement.Fast();

			var lessons = increment.GetLessonsNeeded();

			Assert.Equal(20, lessons);
		}

		[Fact]
		public void ProgressIncrement_GetLessonsNeeded_Slow_ShouldReturn5()
		{
			var increment = ProgressIncrement.Slow();

			var lessons = increment.GetLessonsNeeded();

			Assert.Equal(5, lessons);
		}

		[Fact]
		public void ProgressIncrement_GetLessonsNeeded_Custom_ShouldCalculateCorrectly()
		{
			var increment = ProgressIncrement.Custom(25);

			var lessons = increment.GetLessonsNeeded();

			Assert.Equal(4, lessons); // 100 / 25 = 4 lessons
		}

		[Fact]
		public void ProgressIncrement_GetLessonsNeeded_CustomWithRemainder_ShouldRoundUp()
		{
			var increment = ProgressIncrement.Custom(33);

			var lessons = increment.GetLessonsNeeded();

			Assert.Equal(4, lessons); // ceiling(100 / 33) = 4 lessons
		}

		[Fact]
		public void ProgressIncrement_GetLessonsNeeded_100Percent_ShouldReturn1()
		{
			var increment = ProgressIncrement.Custom(100);

			var lessons = increment.GetLessonsNeeded();

			Assert.Equal(1, lessons); // 100 / 100 = 1 lesson
		}

		[Fact]
		public void ProgressIncrement_GetLessonsNeeded_1Percent_ShouldReturn100()
		{
			var increment = ProgressIncrement.Custom(1);

			var lessons = increment.GetLessonsNeeded();

			Assert.Equal(100, lessons); // 100 / 1 = 100 lessons
		}

		[Fact]
		public void ProgressIncrement_StandardVsFast_FastRequiresMoreLessons()
		{
			var standard = ProgressIncrement.Standard();
			var fast = ProgressIncrement.Fast();

			var standardLessons = standard.GetLessonsNeeded();
			var fastLessons = fast.GetLessonsNeeded();

			Assert.True(fastLessons > standardLessons);
			Assert.Equal(10, standardLessons);
			Assert.Equal(20, fastLessons);
		}

		[Fact]
		public void ProgressIncrement_StandardVsSlow_SlowRequiresFewerLessons()
		{
			var standard = ProgressIncrement.Standard();
			var slow = ProgressIncrement.Slow();

			var standardLessons = standard.GetLessonsNeeded();
			var slowLessons = slow.GetLessonsNeeded();

			Assert.True(slowLessons < standardLessons);
			Assert.Equal(10, standardLessons);
			Assert.Equal(5, slowLessons);
		}

		[Fact]
		public void ProgressIncrement_CanBeCompared()
		{
			var increment1 = ProgressIncrement.Standard();
			var increment2 = ProgressIncrement.Standard();
			var increment3 = ProgressIncrement.Fast();

			Assert.Equal(increment1, increment2);
			Assert.NotEqual(increment1, increment3);
		}
	}
}
