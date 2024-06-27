using Todo.Logic.Domain.Models.Base;

namespace Todo.Logic.Domain.Models {
    public class LabelDto : BaseEntityDto<Guid> {
        public string Name { get; set; }
    }
}