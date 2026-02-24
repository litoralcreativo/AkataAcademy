using AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.Entities
{
	/// <summary>
	/// Represents learning progress in a course with configurable advancement.
	/// Progress is tracked as a percentage (0-100%) and advances based on a configurable increment.
	/// </summary>
	public class Progress : Entity
	{
		public CompletionPercentage Percentage { get; private set; } = null!;
		public ProgressIncrement Increment { get; private set; } = null!;
		public int LessonsCompleted { get; private set; }

		protected Progress() { } // EF

		private Progress(Guid id, CompletionPercentage percentage, ProgressIncrement increment, int lessonsCompleted = 0)
		{
			Id = id;
			Percentage = percentage;
			Increment = increment;
			LessonsCompleted = lessonsCompleted;
		}

		/// <summary>
		/// Creates a new progress instance starting at 0% with standard (10%) increment
		/// </summary>
		internal static Progress Create()
		{
			return CreateWithIncrement(ProgressIncrement.Standard());
		}

		/// <summary>
		/// Creates a new progress instance with a custom increment
		/// </summary>
		internal static Progress CreateWithIncrement(ProgressIncrement increment)
		{
			return new Progress(
				Guid.NewGuid(),
				CompletionPercentage.From(0),
				increment,
				0);
		}

		/// <summary>
		/// Advances progress by the configured increment (or to 100% if next increment exceeds 100%)
		/// </summary>
		internal void Advance()
		{
			var newValue = Percentage.Value + Increment.Value;

			if (newValue > 100)
				newValue = 100;

			Percentage = CompletionPercentage.From(newValue);
			LessonsCompleted++;
		}

		/// <summary>
		/// Determines if the course is fully completed (100% progress)
		/// </summary>
		public bool IsFullyCompleted => Percentage.Value == 100;

		/// <summary>
		/// Calculates remaining lessons needed to complete the course
		/// </summary>
		public int GetRemainingLessons()
		{
			var remainingPercent = 100 - Percentage.Value;
			return (remainingPercent + Increment.Value - 1) / Increment.Value; // Ceiling division
		}

		/// <summary>
		/// Calculates the progress rate (percentage per lesson)
		/// </summary>
		public decimal GetProgressRate() => (decimal)Increment.Value;

		/// <summary>
		/// Calculates estimated lessons to completion based on current progress
		/// </summary>
		public int GetEstimatedLessonsToCompletion()
		{
			if (IsFullyCompleted) return 0;
			return Increment.GetLessonsNeeded() - LessonsCompleted;
		}

		/// <summary>
		/// Calculates completion percentage based on lessons completed
		/// </summary>
		public int CalculateCompletionPercentageFromLessons()
		{
			return Math.Min(100, LessonsCompleted * Increment.Value);
		}
	}
}