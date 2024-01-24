using AutoMapper;
using repository.Interface;
using service.Models;


namespace service.Services
{
    /// <summary>
    /// Service class responsible for managing user-related operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IJobRepository jobRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the UserService class.
        /// </summary>
        /// <param name="_repository">An instance of the IUserRepository interface for user-related operations.</param>
        /// <param name="mapper">An instance of the IMapper interface for object mapping.</param>
        /// <param name="jobRepository">An instance of the IJobRepository interface for job-related operations.</param>
        public UserService(IUserRepository _repository, IMapper mapper, IJobRepository jobRepository)
        {
            this.userRepository = _repository;
            this._mapper = mapper;
            this.jobRepository = jobRepository;
        }

        /// <summary>
        /// Retrieves all users from the repository.
        /// </summary>
        /// <returns>Returns a Task<List<User>> representing a list of all users.</returns>
        public async Task<List<User>> GetAll()
        {
            // Retrieve all users from the userRepository.
            var users = userRepository.GetUsers();

            // Map the retrieved users to a List<User> using AutoMapper.
            return _mapper.Map<List<User>>(users);
        }

        /// <summary>
        /// Retrieves jobs associated with a specific user.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        /// <returns>Returns a Task<List<Job>> representing a list of jobs associated with the user.</returns>
        public async Task<List<Job>> GetUserJobs(Guid userId)
        {
            // Retrieve jobs associated with the user from the jobRepository.
            var jobs = jobRepository.GetUserJobs(userId);

            // Map the retrieved jobs to a List<Job> using AutoMapper.
            return _mapper.Map<List<Job>>(jobs);
        }
    }
}