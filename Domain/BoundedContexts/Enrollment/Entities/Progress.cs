using AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.Entities
{
	public class Progress : Entity
	{
		public CompletionPercentage Percentage { get; private set; } = null!;

		protected Progress() { } // EF

		private Progress(Guid id, CompletionPercentage percentage)
		{
			Id = id;
			Percentage = percentage;
		}

		internal static Progress Create()
		{
			return new Progress(
				Guid.NewGuid(),
				new CompletionPercentage(0));
		}

		internal void Advance()
		{
			var newValue = Percentage.Value + 10;

			if (newValue > 100)
				newValue = 100;

			Percentage = new CompletionPercentage(newValue);
		}
	}
}