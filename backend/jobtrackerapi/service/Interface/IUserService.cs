using service.Models;

namespace service
{
    /// <summary>
    /// Interface defining the contract for a service that manages user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves a list of all users and returns a Task<List<User>>.
        /// </summary>
        /// <returns>Returns a Task<List<User>> representing the list of all users.</returns>
        public Task<List<User>> GetAll();

        /// <summary>
        /// Retrieves a list of jobs associated with a specific user and returns a Task<List<Job>>.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        /// <returns>Returns a Task<List<Job>> representing the list of jobs associated with the user.</returns>
        public Task<List<Job>> GetUserJobs(Guid userId);
    }
}
