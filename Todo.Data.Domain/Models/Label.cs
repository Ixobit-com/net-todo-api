using Todo.Data.Domain.Models.Base;

namespace Todo.Data.Domain.Models {
    public class Label : BaseAuditableEntity<Guid> {
        public string Name { get; set; }

        public virtual ICollection<TicketLabel> Tasks { get; set; }
    }
}