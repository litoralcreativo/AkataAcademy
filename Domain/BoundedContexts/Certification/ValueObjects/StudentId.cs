using Domain.Common;

namespace Domain.BoundedContexts.Certification.ValueObjects
{
    public class StudentId : ValueObject
    {
        public Guid Value { get; private set; }

        protected StudentId() { }

        public StudentId(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("StudentId inválido");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}