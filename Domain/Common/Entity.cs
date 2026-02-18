using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        private List<IDomainEvent>? _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents
        {
            get
            {
                return _domainEvents == null
                    ? new List<IDomainEvent>().AsReadOnly()
                    : _domainEvents.AsReadOnly();
            }
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (_domainEvents == null)
            {
                _domainEvents = new List<IDomainEvent>();
            }

            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            if (_domainEvents != null)
            {
                _domainEvents.Clear();
            }
        }

        #region Equality

        public override bool Equals(object? obj)
        {
            var other = obj as Entity;
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id == Guid.Empty || other.Id == Guid.Empty)
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        #endregion
    }
}
