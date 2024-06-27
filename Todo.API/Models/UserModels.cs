namespace Todo.API.Models {
    /// <summary>
    /// Represents model to provide base user data
    /// </summary>
    public class UserModel {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets user email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Gets or sets user first name
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// Gets or sets user last name
        /// </summary>
        public string? LastName { get; set; }
    }

    /// <summary>
    /// Represents model to provide detailed user data
    /// </summary>
    public class UserDetailsModel {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets user email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Gets or sets user first name
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// Gets or sets user last name
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// Gets or sets user roles
        /// </summary>
        public IEnumerable<RoleKeyValueModel> Roles { get; set; }
    }

    /// <summary>
    /// Represents model to create new user
    /// </summary>
    public class UserCreateModel {
        /// <summary>
        /// Gets or sets user email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Gets or sets user first name
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// Gets or sets user last name
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// Gets or sets company id
        /// </summary>
        public Guid? CompanyId { get; set; }
        /// <summary>
        /// Gets or sets user roles
        /// </summary>
        public IEnumerable<Guid> RoleIds { get; set; }
    }

    /// <summary>
    /// Represents model to update user
    /// </summary>
    public class UserUpdateModel : UserCreateModel {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents short user model
    /// </summary>
    public class UserKeyValueModel {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets user full name
        /// </summary>
        public string? FullName { get; set; }
    }
}