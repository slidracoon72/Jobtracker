using Microsoft.AspNetCore.Mvc;
using service.Interface;
using service.Models;

namespace jobtrackerapi.Controllers
{
    // This class represents a controller for managing job-related operations in a web API.
    [ApiController]
    [Route("/Job")]
    public class JobController : Controller
    {
        private readonly IJobService service;

        // Constructor for the JobController class, which injects an instance of IJobService.
        public JobController(IJobService _jobService)
        {
            service = _jobService;
        }

        // This HTTP POST action method is used to add a job to the system.
        [GoogleTokenValidation]
        [HttpPost]
        public Job AddJob(Job job)
        {
            // Call the AddJob method from the injected service to add the job and return the result.
            // Note that .Result is used to block and get the result from the asynchronous operation.
            return service.AddJob(job).Result;
        }

        [GoogleTokenValidation]
        [HttpGet("GetStatus")]
        public Dictionary<string,string> GetStatuses(Guid userId)
        {
            return service.GetStatuses(userId).Result;
        }

        // This HTTP DELETE action method is used to delete a job by its ID.
        [GoogleTokenValidation]
        [HttpDelete("{id}")]
        public bool DeleteJob(Guid id)
        {
            try
            {
                // Call the DeleteJob method from the injected service to delete the job.
                bool deletionResult = service.DeleteJob(id);

                return deletionResult;
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error response in case of an exception.
                return false;
            }
        }

        // This HTTP GET action method is used to retrieve a job by its ID.
        [GoogleTokenValidation]
        [HttpGet("{id}")]
        public Job GetJob(Guid id)
        {
            try
            {
                var job = service.GetJob(id).Result;

                if (job != null)
                {
                    return job; // Return a 200 OK response with the job if found.
                }
                else
                {
                    return new Job(); // Return a 404 Not Found if the job is not found.
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception based on your application's requirements
                return new Job();
            }
        }

        [GoogleTokenValidation]
        [HttpPut("{id}")]
        public Job UpdateJob(Guid id, int status)
        {
            try
            {
                var jobFromDb = service.UpdateJob(id,status).Result;

                if (jobFromDb != null)
                {
                    return jobFromDb; // Return a 200 OK response with the job if found.
                }
                else
                {
                    return new Job(); // Return a 404 Not Found if the job is not found.
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception based on your application's requirements
                return new Job();
            }
        }

        [GoogleTokenValidation]
        [HttpGet("/locations/{id}")]
        public List<string> GetLocations(Guid id)
        {
            return service.GetLocations(id);
        }
    }
}
