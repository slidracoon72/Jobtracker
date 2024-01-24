using database.Models;

namespace repository.Interface
{
    /// <summary>
    /// Interface defining the contract for interacting with job-related data in a repository.
    /// </summary>
    public interface IJobRepository
    {
        /// <summary>
        /// Adds a job to the repository and returns the added job.
        /// </summary>
        /// <param name="job">Job object to be added.</param>
        /// <returns>Returns the added Job object.</returns>
        public Job AddJob(Job job);

        /// <summary>
        /// Retrieves a job by its unique identifier (ID).
        /// </summary>
        /// <param name="id">Unique identifier of the job.</param>
        /// <returns>Returns the Job object associated with the specified ID.</returns>
        public Job GetJob(Guid id);

        /// <summary>
        /// Retrieves a list of jobs associated with a specific user.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        /// <returns>Returns a list of Job objects associated with the specified user.</returns>
        public List<Job> GetUsersJob(Guid userId);

        /// <summary>
        /// Updates a job based on its unique identifier and returns the updated job.
        /// </summary>
        /// <param name="id">Unique identifier of the job to be updated.</param>
        /// <param name="job">Updated Job object.</param>
        /// <returns>Returns the updated Job object.</returns>
        public Job UpdateJob(Guid id, int status);

        /// <summary>
        /// Deletes a job based on its unique identifier and returns true if the operation was successful.
        /// </summary>
        /// <param name="id">Unique identifier of the job to be deleted.</param>
        /// <returns>Returns true if the deletion was successful; otherwise, false.</returns>
        public bool DeleteJob(Guid id);

        /// <summary>
        /// Retrieves a list of jobs associated with a specific user.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        /// <returns>Returns a list of Job objects associated with the specified user.</returns>
        public List<Job> GetUserJobs(Guid userId);

        /// <summary>
        /// Retrieves job statuses associated with a specific user.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        /// <returns>Returns a dictionary of job statuses with their descriptions.</returns>
        public Dictionary<string, string> GetStatuses(Guid userId);

        /// <summary>
        /// Retrieves job locations associated with a specific user.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        /// <returns>Returns a list of unique job locations.</returns>
        public List<string> GetLocations(Guid userId);
    }
}
