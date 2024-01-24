using System;
using service.Models;

namespace service.Interface
{
    /// <summary>
    /// Interface defining the contract for a service that manages job-related operations.
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// Adds a job using the provided job object and returns a Task representing the added job.
        /// </summary>
        /// <param name="job">Job object to be added.</param>
        /// <returns>Returns a Task<Job> representing the added job.</returns>
        public Task<Job> AddJob(Job job);

        /// <summary>
        /// Retrieves a job by its unique identifier (ID) and returns a Task<Job>.
        /// </summary>
        /// <param name="id">Unique identifier of the job.</param>
        /// <returns>Returns a Task<Job> representing the retrieved job.</returns>
        public Task<Job> GetJob(Guid id);

        /// <summary>
        /// Deletes a job by its ID and returns a boolean indicating the success of the operation.
        /// </summary>
        /// <param name="id">Unique identifier of the job to be deleted.</param>
        /// <returns>Returns a boolean indicating the success of the deletion.</returns>
        public bool DeleteJob(Guid id);

        /// <summary>
        /// Retrieves job statuses associated with a specific user.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        /// <returns>Returns a Task<Dictionary<string, string>> representing job statuses with their descriptions.</returns>
        public Task<Dictionary<string, string>> GetStatuses(Guid userId);

        /// <summary>
        /// Updates a job based on its unique identifier and returns a Task<Job> representing the updated job.
        /// </summary>
        /// <param name="id">Unique identifier of the job to be updated.</param>
        /// <param name="job">Updated Job object.</param>
        /// <returns>Returns a Task<Job> representing the updated job.</returns>
        public Task<Job> UpdateJob(Guid id,int status);

        /// <summary>
        /// Retrieves job locations associated with a specific user.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        /// <returns>Returns a List<string> representing unique job locations.</returns>
        public List<string> GetLocations(Guid userId);
    }
}
