using Todo.Data.Domain.Contracts;

namespace Todo.Data.Domain.Models.Base {
    public abstract class BaseEntity<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey> {
        public TKey Id { get; set; }
    }
}