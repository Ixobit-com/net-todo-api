using Todo.Data.Domain.Models.Base;

namespace Todo.Data.Domain.Models.Identity {
    public class Client : BaseAuditableEntity<Guid> {
        public string? Name { get; set; }
        public string? Key { get; set; }
        public string? SecretSalt { get; set; }
        public string? SecretHash { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ClientScope>? Scopes { get; set; }
    }
}