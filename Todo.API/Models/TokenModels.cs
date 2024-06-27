namespace Todo.API.Models {
    /// <summary>
    /// Represents token request model
    /// </summary>
    public class AuthModel {
        /// <summary>
        /// Gets or sets grant type
        /// </summary>
        public string? GrantType { get; set; }
        /// <summary>
        /// Gets or sets client key
        /// </summary>
        public string? ClientKey { get; set; }
        /// <summary>
        /// Gets or sets client secret
        /// </summary>
        public string? ClientSecret { get; set; }
        /// <summary>
        /// Gets or sets user email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Gets or sets user password
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// Gets or sets refresh token
        /// </summary>
        public string? RefreshToken { get; set; }
    }

    /// <summary>
    /// Represents access token information
    /// </summary>
    public class AccessTokenModel {
        /// <summary>
        /// Gets or sets access token
        /// </summary>
        public string? AccessToken { get; set; }
        /// <summary>
        /// Gets or sets access token expiration time
        /// </summary>
        public int ExpireIn { get; set; }
        /// <summary>
        /// Gets or sets refresh token
        /// </summary>
        public string? RefreshToken { get; set; }
    }
}