using Todo.Data.Domain.Models.Base;

namespace Todo.Data.Domain.Models.Identity {
    public class Scope : BaseEntity<Guid> {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<ClientScope>? Clients { get; set; }
    }
}