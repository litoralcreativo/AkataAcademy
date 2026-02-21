using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects
{
    public record CompletionPercentage : IValueObject
    {
        public int Value { get; init; }

        // Parameterless constructor for EF Core
        protected CompletionPercentage() { }

        public CompletionPercentage(int value)
        {
            if (value < 0 || value > 100)
                throw new DomainException("Invalid completion percentage. Must be between 0 and 100.");

            Value = value;
        }
    }
}