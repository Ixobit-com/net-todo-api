using Todo.Logic.Domain.Models.Base;

namespace Todo.Logic.Domain.Models {
    public class ScopeDto : BaseEntityDto<Guid> {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}