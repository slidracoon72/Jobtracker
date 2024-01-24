namespace service.Models
{
    /// <summary>
    /// Model class representing Single Sign-On information.
    /// </summary>
    public class SingleSignOn
    {
        /// <summary>
        /// Unique identifier for Single Sign-On.
        /// </summary>
        public Guid Id { get; set; } // Consider adding a primary key

        /// <summary>
        /// Unique identifier associated with the user in the external authentication provider.
        /// </summary>
        public string? Uid { get; set; }

        /// <summary>
        /// Email address associated with the user in Single Sign-On.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Indicates whether the email associated with the user is verified.
        /// </summary>
        public bool EmailVerified { get; set; }

        /// <summary>
        /// Indicates whether the user is anonymous.
        /// </summary>
        public bool IsAnonymous { get; set; }

        /// <summary>
        /// URL of the user's profile photo.
        /// </summary>
        public string? PhotoURL { get; set; }

        /// <summary>
        /// List of additional data provided by the authentication provider.
        /// </summary>
        public List<ProviderData> ProviderData { get; set; }

        /// <summary>
        /// Refresh token used for obtaining new access tokens.
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Access token used for authenticating API requests.
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// Time, in seconds, until the access token expires.
        /// </summary>
        public int OauthExpireIn { get; set; }

        /// <summary>
        /// Time, in seconds, until the Single Sign-On information expires.
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Raw user information provided by the authentication provider.
        /// </summary>
        public string? RawUserInfo { get; set; }

        /// <summary>
        /// Type or category of the Single Sign-On operation.
        /// </summary>
        public string? Kind { get; set; }

        /// <summary>
        /// Type of operation being performed (e.g., login, signup).
        /// </summary>
        public string? OperationType { get; set; }

        /// <summary>
        /// Foreign key linking to the user in the service layer.
        /// </summary>
        public Guid UserId { get; set; } // Foreign key
    }

    /// <summary>
    /// Model class representing additional data provided by the authentication provider.
    /// </summary>
    public class ProviderData
    {
        /// <summary>
        /// Unique identifier for ProviderData.
        /// </summary>
        public Guid Id { get; set; } // Consider adding a primary key

        /// <summary>
        /// Identifier associated with the authentication provider.
        /// </summary>
        public string? ProviderId { get; set; }

        /// <summary>
        /// Unique identifier associated with the user in the authentication provider.
        /// </summary>
        public string? Uid { get; set; }

        /// <summary>
        /// Display name associated with the user in the authentication provider.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Email address associated with the user in ProviderData.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Phone number associated with the user in ProviderData.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// URL of the user's profile photo in ProviderData.
        /// </summary>
        public string? PhotoURL { get; set; }

        /// <summary>
        /// Foreign key linking to the associated SingleSignOn.
        /// </summary>
        public Guid SingleSignOnId { get; set; } // Foreign key
    }
}
