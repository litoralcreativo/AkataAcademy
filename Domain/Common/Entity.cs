namespace AkataAcademy.Domain.Common
{
    public abstract class Entity<TId> : IHasId<TId>
    {
        public TId Id { get; protected set; } = default!;

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
            var other = obj as Entity<TId>;
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            // Comparación de Id genérica
            if (EqualityComparer<TId>.Default.Equals(Id, default!) || EqualityComparer<TId>.Default.Equals(other.Id, default!))
                return false;

            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }

        #endregion
    }

    // Versión por defecto usando int como tipo de Id
    public abstract class Entity : Entity<Guid>
    {
    }
}
