using Todo.Logic.Domain.Models.Base;

namespace Todo.Logic.Domain.Models {
    public class RoleDto : BaseEntityDto<Guid> {
        public string Name { get; set; }
    }
}