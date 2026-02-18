using Domain.Common;

namespace Domain.BoundedContexts.Certification.ValueObjects
{
    public class CourseId : ValueObject
    {
        public Guid Value { get; private set; }

        protected CourseId() { }

        public CourseId(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("CourseId inválido");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}