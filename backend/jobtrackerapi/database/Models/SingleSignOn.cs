namespace database.Models
{
    /// <summary>
    /// Represents a user's Single Sign-On information.
    /// </summary>
    public class SingleSignOn
    {
        /// <summary>
        /// Gets or sets the unique identifier for the Single Sign-On record.
        /// </summary>
        public Guid Id { get; set; } // Consider adding a primary key

        /// <summary>
        /// Gets or sets the unique identifier associated with the user in the external authentication provider.
        /// </summary>
        public string? Uid { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the user's email address has been verified.
        /// </summary>
        public bool EmailVerified { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the user is anonymous.
        /// </summary>
        public bool IsAnonymous { get; set; }

        /// <summary>
        /// Gets or sets the URL of the user's photo.
        /// </summary>
        public string? PhotoURL { get; set; }

        /// <summary>
        /// Gets or sets the list of provider data associated with the Single Sign-On.
        /// </summary>
        public List<ProviderData> ProviderData { get; set; }

        /// <summary>
        /// Gets or sets the refresh token for the Single Sign-On.
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the access token for the Single Sign-On.
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the expiration time (in seconds) for the access token in OAuth.
        /// </summary>
        public int OauthExpireIn { get; set; }

        /// <summary>
        /// Gets or sets the expiration time (in seconds) for the Single Sign-On.
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Gets or sets the raw user information.
        /// </summary>
        public string? RawUserInfo { get; set; }

        /// <summary>
        /// Gets or sets the kind of operation associated with the Single Sign-On.
        /// </summary>
        public string? Kind { get; set; }

        /// <summary>
        /// Gets or sets the type of operation associated with the Single Sign-On.
        /// </summary>
        public string? OperationType { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated user (foreign key).
        /// </summary>
        public Guid UserId { get; set; } // Foreign key

        /// <summary>
        /// Gets or sets the associated User entity (navigation property).
        /// </summary>
        public User User { get; set; }
    }

    /// <summary>
    /// Represents additional data from an authentication provider.
    /// </summary>
    public class ProviderData
    {
        /// <summary>
        /// Gets or sets the unique identifier for the ProviderData record.
        /// </summary>
        public Guid Id { get; set; } // Consider adding a primary key

        /// <summary>
        /// Gets or sets the unique identifier of the authentication provider.
        /// </summary>
        public string? ProviderId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier associated with the user in the authentication provider.
        /// </summary>
        public string? Uid { get; set; }

        /// <summary>
        /// Gets or sets the display name associated with the user in the authentication provider.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the email address associated with the user in the authentication provider.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number associated with the user in the authentication provider.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the URL of the user's photo.
        /// </summary>
        public string? PhotoURL { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated Single Sign-On record (foreign key).
        /// </summary>
        public Guid SingleSignOnId { get; set; } // Foreign key

        /// <summary>
        /// Gets or sets the associated SingleSignOn entity (navigation property).
        /// </summary>
        public SingleSignOn SingleSignOn { get; set; }
    }
}
