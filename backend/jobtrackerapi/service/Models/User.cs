namespace service.Models
{
    /// <summary>
    /// Model class representing user information.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Email address associated with the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Date and time when the user was created.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Date and time when the user was last updated.
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}
