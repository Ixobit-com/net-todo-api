using Todo.Common.Enums;

namespace Todo.API.Models {
    /// <summary>
    /// Represents model to provide base ticket data
    /// </summary>
    public class TicketModel {
        /// <summary>
        /// Gets or sets ticket id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets ticket subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Gets or sets ticket status
        /// </summary>
        public TicketStatus Status { get; set; }
        /// <summary>
        /// Gets or sets ticket priority
        /// </summary>
        public TicketPriority Priority { get; set; }
        /// <summary>
        /// Gets or sets sprint
        /// </summary>
        public SprintKeyValueModel Sprint { get; set; }
        /// <summary>
        /// Gets or sets assignee
        /// </summary>
        public UserKeyValueModel Assignee { get; set; }
        /// <summary>
        /// Gets or sets ticket labels
        /// </summary>
        public IEnumerable<LabelKeyValueModel> Labels { get; set; }
    }

    /// <summary>
    /// Represents model to provide detailed ticket data
    /// </summary>
    public class TicketDetailsModel {
        /// <summary>
        /// Gets or sets ticket id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets ticket subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Gets or sets ticket description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets ticket status
        /// </summary>
        public TicketStatus Status { get; set; }
        /// <summary>
        /// Gets or sets ticket priority
        /// </summary>
        public TicketPriority Priority { get; set; }
        /// <summary>
        /// Gets or sets sprint
        /// </summary>
        public SprintKeyValueModel Sprint { get; set; }
        /// <summary>
        /// Gets or sets parent ticket
        /// </summary>
        public TicketKeyValueModel ParentTicket { get; set; }
        /// <summary>
        /// Gets or sets assignee
        /// </summary>
        public UserKeyValueModel Assignee { get; set; }
        /// <summary>
        /// Gets or sets ticket labels
        /// </summary>
        public IEnumerable<LabelKeyValueModel> Labels { get; set; }
    }

    /// <summary>
    /// Represents model to create new ticket
    /// </summary>
    public class TicketCreateModel {
        /// <summary>
        /// Gets or sets ticket subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Gets or sets ticket description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets ticket status
        /// </summary>
        public TicketStatus Status { get; set; }
        /// <summary>
        /// Gets or sets ticket priority
        /// </summary>
        public TicketPriority Priority { get; set; }
        /// <summary>
        /// Gets or sets sprint id
        /// </summary>
        public Guid? SprintId { get; set; }
        /// <summary>
        /// Gets or sets parent ticket id
        /// </summary>
        public Guid? ParentTicketId { get; set; }
        /// <summary>
        /// Gets or sets assignee id
        /// </summary>
        public Guid? AssigneeId { get; set; }
        /// <summary>
        /// Gets or sets ticket labels
        /// </summary>
        public IEnumerable<Guid> LabelIds { get; set; }
    }

    /// <summary>
    /// Represents model to update ticket
    /// </summary>
    public class TicketUpdateModel : TicketCreateModel {
        /// <summary>
        /// Gets or sets ticket id
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents short ticket model
    /// </summary>
    public class TicketKeyValueModel {
        /// <summary>
        /// Gets or sets ticket id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets ticket subject
        /// </summary>
        public string Subject { get; set; }
    }
}