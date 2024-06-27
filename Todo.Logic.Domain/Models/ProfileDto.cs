using Todo.Logic.Domain.Models.Base;

namespace Todo.Logic.Domain.Models {
    public class ProfileDto : BaseEntityDto<Guid> {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}