namespace Todo.API.Models {
    /// <summary>
    /// Represents model to provide base client data
    /// </summary>
    public class ClientModel {
        /// <summary>
        /// Gets or sets client id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets client name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets is client active
        /// </summary>
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Represents model to provide detailed client data
    /// </summary>
    public class ClientDetailsModel {
        /// <summary>
        /// Gets or sets client id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets client name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets is client active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets client scopes
        /// </summary>
        public IEnumerable<ScopeKeyValueModel>? Scopes { get; set; }
    }

    /// <summary>
    /// Represents model to create new client
    /// </summary>
    public class ClientCreateModel {
        /// <summary>
        /// Gets or sets client name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets is client active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets scope ids
        /// </summary>
        public IEnumerable<Guid>? ScopeIds { get; set; }
    }

    /// <summary>
    /// Represents model to update client
    /// </summary>
    public class ClientUpdateModel : ClientCreateModel {
        /// <summary>
        /// Gets or sets client id
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents short client model
    /// </summary>
    public class ClientKeyValueModel {
        /// <summary>
        /// Gets or sets client id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets client name
        /// </summary>
        public string? Name { get; set; }
    }
}