using service.Models;

namespace service.Interface
{
    /// <summary>
    /// Interface defining the contract for authentication-related services.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Performs authentication based on Single Sign-On information.
        /// </summary>
        /// <param name="singleSignOn">SingleSignOn object containing authentication information.</param>
        /// <returns>Returns a Task representing the asynchronous operation with a User object representing the authenticated user.</returns>
        public Task<User> auth(SingleSignOn singleSignOn);
    }
}
