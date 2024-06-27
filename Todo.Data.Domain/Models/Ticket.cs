using Todo.Common.Enums;
using Todo.Data.Domain.Models.Base;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.Domain.Models {
    public class Ticket : BaseAuditableEntity<Guid> {
        public string Subject { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public Guid? SprintId { get; set; }
        public Guid? ParentTicketId { get; set; }
        public Guid? AssigneeId { get; set; }

        public virtual Sprint Sprint { get; set; }
        public virtual Ticket ParentTicket { get; set; }
        public virtual User Assignee { get; set; }
        public virtual ICollection<Ticket> SubTickets { get; set; }
        public virtual ICollection<TicketLabel> Labels { get; set; }
    }
}