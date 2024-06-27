using Todo.Data.Domain.Models.Base;

namespace Todo.Data.Domain.Models {
    public class Sprint : BaseAuditableEntity<Guid> {
        public string Name { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}