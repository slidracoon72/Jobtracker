using AutoMapper;
using repository.Interface;
using service.Interface;
using service.Models;


namespace service.Services
{
    // This class represents a service for managing job-related operations.
    public class JobService : IJobService
    {
        private readonly IJobRepository jobRepository;
        private readonly IMapper _mapper;

        // Constructor for the JobService class, which injects IJobRepository and IMapper.
        public JobService(IJobRepository _repository, IMapper mapper)
        {
            this.jobRepository = _repository;
            this._mapper = mapper;
        }

        // Adds a job to the service by mapping it between service and repository layers and returns the added job.
        public async Task<Job> AddJob(Job job)
        {
            // Map the service layer Job object to a repository layer Job object, add it to the repository, and then map it back.
            return _mapper.Map<service.Models.Job>(jobRepository.AddJob(_mapper.Map<database.Models.Job>(job)));
        }

        // This method deletes a job by its unique identifier (ID).
        // It delegates the deletion operation to the jobRepository and returns the result.
        public bool DeleteJob(Guid id)
        {
            return jobRepository.DeleteJob(id);
        }

        // This asynchronous method retrieves a job by its unique identifier (ID).
        // It delegates the retrieval operation to the jobRepository, maps the result using AutoMapper,
        // and returns a Task<Job> representing the retrieved job.
        public async Task<Job> GetJob(Guid id)
        {
            try
            {
                // Retrieve the job from the jobRepository based on the provided ID.
                var job = jobRepository.GetJob(id);

                // Map the retrieved job to a Job model using AutoMapper.
                return _mapper.Map<Job>(job);
            }
            catch (Exception ex)
            {
                return new Job();
            }
        }

        public Task<Dictionary<string, string>> GetStatuses(Guid userId)
        {
            return Task.FromResult(jobRepository.GetStatuses(userId));
        }

        public async Task<Job> UpdateJob(Guid id, int status)
        {
            try
            {
                // Retrieve the job from the jobRepository based on the provided ID.
                var jobFromDb = jobRepository.UpdateJob(id,status);

                // Map the retrieved job to a Job model using AutoMapper.
                return _mapper.Map<Job>(jobFromDb);
            }
            catch (Exception ex)
            {
                return new Job();
            }
        }

        public List<string> GetLocations(Guid userId)
        {
            return jobRepository.GetLocations(userId);
        }
    }
}