namespace Todo.Data.Domain.Models {
    public class TicketLabel {
        public Guid TicketId { get; set; }
        public Guid LabelId { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual Label Label { get; set; }
    }
}