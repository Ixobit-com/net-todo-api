using Todo.Logic.Domain.Models.Base;

namespace Todo.Logic.Domain.Models {
    public interface IHasRoles {
        IEnumerable<RoleDto> Roles { get; set; }
    }

    public class UserDto : BaseEntityDto<Guid>, IHasRoles {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}