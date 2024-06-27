using Microsoft.AspNetCore.Identity;

namespace Todo.Data.Domain.Models.Identity {
    public class UserToken : IdentityUserToken<Guid> { }
}