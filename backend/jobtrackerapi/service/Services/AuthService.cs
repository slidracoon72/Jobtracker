using AutoMapper;
using repository.Interface;
using service.Interface;
using service.Models;

namespace service.Services
{
    /// <summary>
    /// Service class responsible for authentication-related operations.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the AuthService class.
        /// </summary>
        /// <param name="_repository">An instance of the IAuthRepository interface for authentication operations.</param>
        /// <param name="mapper">An instance of the IMapper interface for object mapping.</param>
        public AuthService(IAuthRepository _repository, IMapper mapper)
        {
            this.authRepository = _repository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Authenticates a user based on Single Sign-On information.
        /// </summary>
        /// <param name="singleSignOn">SingleSignOn object containing authentication information.</param>
        /// <returns>Returns a Task<User> representing the authenticated user in the service layer.</returns>
        public async Task<User> auth(SingleSignOn singleSignOn)
        {
            // Use AutoMapper to map the SingleSignOn from the service layer to the database layer,
            // then perform authentication and map the result back to the service layer User model.
            return _mapper.Map<service.Models.User>(authRepository.auth(_mapper.Map<database.Models.SingleSignOn>(singleSignOn)));
        }
    }
}
