namespace Todo.API.Models {
    /// <summary>
    /// Represents model to provide profile data
    /// </summary>
    public class ProfileModel {
        /// <summary>
        /// Gets or sets user email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets user first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets user last name
        /// </summary>
        public string LastName { get; set; }
    }

    /// <summary>
    /// Represents model to update profile data
    /// </summary>
    public class ProfileUpdateModel {
        /// <summary>
        /// Gets or sets user first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets user last name
        /// </summary>
        public string LastName { get; set; }
    }

    /// <summary>
    /// Represents model to trigger reset password
    /// </summary>
    public class ForgotPasswordModel {
        /// <summary>
        /// Gets or sets user email
        /// </summary>
        public string Email { get; set; }
    }

    /// <summary>
    /// Represents model to reset password
    /// </summary>
    public class ResetPasswordModel {
        /// <summary>
        /// Gets or sets user email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets user password
        /// </summary>
        public string NewPassword { get; set; }
        /// <summary>
        /// Gets or sets verification token
        /// </summary>
        public string Token { get; set; }
    }

    /// <summary>
    /// Represents model to change password
    /// </summary>
    public class ChangePasswordModel {
        /// <summary>
        /// Gets or sets current password
        /// </summary>
        public string CurrentPassword { get; set; }
        /// <summary>
        /// Gets or sets new password
        /// </summary>
        public string NewPassword { get; set; }
    }
}