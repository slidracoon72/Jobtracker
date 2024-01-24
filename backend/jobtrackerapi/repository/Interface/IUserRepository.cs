using database.Models;

namespace repository.Interface
{
    /// <summary>
    /// Interface defining the contract for interacting with user-related data in a repository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>Returns a list of User objects representing all users.</returns>
        public List<User> GetUsers();
    }
}
