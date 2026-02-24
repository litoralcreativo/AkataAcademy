using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects
{
	/// <summary>
	/// Represents the percentage increment when a lesson is completed.
	/// Configurable to allow different progression speeds.
	/// </summary>
	public record ProgressIncrement(int Value) : IValueObject<ProgressIncrement, int>
	{
		public static ProgressIncrement From(int value)
		{
			if (value <= 0)
				throw new DomainException("Progress increment must be greater than 0.");

			if (value > 100)
				throw new DomainException("Progress increment cannot exceed 100.");

			return new ProgressIncrement(value);
		}

		/// <summary>
		/// Standard increment: 10% per lesson (10 lessons to complete)
		/// </summary>
		public static ProgressIncrement Standard() => new(10);

		/// <summary>
		/// Fast increment: 5% per lesson (20 lessons to complete)
		/// </summary>
		public static ProgressIncrement Fast() => new(5);

		/// <summary>
		/// Slow increment: 20% per lesson (5 lessons to complete)
		/// </summary>
		public static ProgressIncrement Slow() => new(20);

		/// <summary>
		/// Custom increment
		/// </summary>
		public static ProgressIncrement Custom(int percentPerLesson) => From(percentPerLesson);

		/// <summary>
		/// Calculates how many lessons are needed to complete the course
		/// </summary>
		public int GetLessonsNeeded() => (100 + Value - 1) / Value; // Ceiling division
	}
}
