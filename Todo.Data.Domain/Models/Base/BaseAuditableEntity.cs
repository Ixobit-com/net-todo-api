using Todo.Data.Domain.Contracts;

namespace Todo.Data.Domain.Models.Base {
    public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>, IAuditableEntity
        where TKey : IEquatable<TKey> {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}