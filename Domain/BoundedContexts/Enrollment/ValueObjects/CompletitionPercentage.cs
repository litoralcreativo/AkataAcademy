using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects
{
    public record CompletionPercentage(int Value) : IValueObject<CompletionPercentage, int>
    {
        public static CompletionPercentage From(int value)
        {
            if (value < 0 || value > 100)
                throw new DomainException("Invalid completion percentage. Must be between 0 and 100.");
            return new CompletionPercentage(value);
        }
    }
}