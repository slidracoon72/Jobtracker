using database.context;
using database.Models;
using repository.Interface;

namespace repository.Repositories
{
    /// <summary>
    /// Repository class for handling user-related operations.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly DbContext dbContext;

        /// <summary>
        /// Constructor for the UserRepository class, which injects a DbContext.
        /// </summary>
        /// <param name="dbContext">An instance of the DbContext.</param>
        public UserRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves a list of all users from the repository.
        /// </summary>
        /// <returns>Returns a list of User objects representing all users.</returns>
        public List<User> GetUsers()
        {
            // Retrieve and return all users from the repository.
            return dbContext.User.ToList();
        }
    }
}
