using Todo.Logic.Domain.Models.Base;
using Todo.Common.Enums;

namespace Todo.Logic.Domain.Models {
    public class TicketDto : BaseEntityDto<Guid> {
        public string Subject { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public SprintDto Sprint { get; set; }
        public TicketDto ParentTicket { get; set; }
        public UserDto Assignee { get; set; }
        public IEnumerable<LabelDto> Labels { get; set; }
    }
}