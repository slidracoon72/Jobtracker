using database.Models;

namespace repository.Interface
{
    /// <summary>
    /// Interface for the authentication repository.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Authenticates a user based on Single Sign-On information.
        /// </summary>
        /// <param name="singleSignOn">SingleSignOn object containing authentication information.</param>
        /// <returns>Returns a User object representing the authenticated user.</returns>
        public User auth(SingleSignOn singleSignOn);
    }
}
