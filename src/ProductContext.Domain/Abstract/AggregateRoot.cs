using System;
using System.Collections.Generic;

namespace ProductContext.Domain.Abstract
{
    public interface IAggregateRootEntity
    {
        string Id { get; }
        int Version { get; }
        void ClearEvents();
        IEnumerable<object> GetEvents();
        bool HasEvents();

    }
    
    public abstract class AggregateRootEntity<TId>
        : IAggregateRootEntity, IEquatable<AggregateRootEntity<TId>>
    {
        private readonly IList<object> _events = new List<object>();

        public virtual TId Id { get; protected set; }

        string IAggregateRootEntity.Id => Id.ToString();

        public virtual int Version { get; protected set; } = -1;

        IEnumerable<object> IAggregateRootEntity.GetEvents()
        {
            return _events;
        }

        bool IAggregateRootEntity.HasEvents()
        {
            return _events.Count > 0;
        }

        public void RaiseEvent(object @event)
        {
            _events.Add(@event);
        }

        void IAggregateRootEntity.ClearEvents()
        {
            _events.Clear();
        }

        public virtual bool Equals(AggregateRootEntity<TId> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((AggregateRootEntity<TId>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }

        public static bool operator ==(AggregateRootEntity<TId> left, AggregateRootEntity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AggregateRootEntity<TId> left, AggregateRootEntity<TId> right)
        {
            return !Equals(left, right);
        }
    }
}