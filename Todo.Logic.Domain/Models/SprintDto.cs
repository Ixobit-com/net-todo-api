using Todo.Logic.Domain.Models.Base;

namespace Todo.Logic.Domain.Models {
    public class SprintDto : BaseEntityDto<Guid> {
        public string Name { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}