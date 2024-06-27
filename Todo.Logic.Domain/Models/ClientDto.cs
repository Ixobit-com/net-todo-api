using Todo.Logic.Domain.Models.Base;

namespace Todo.Logic.Domain.Models {
    public class ClientDto : BaseEntityDto<Guid> {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Secret { get; set; }
        public IEnumerable<ScopeDto>? Scopes { get; set; }
    }
}