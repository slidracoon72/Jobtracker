using Microsoft.AspNetCore.Mvc;
using service;

namespace jobtrackerapi.Controllers
{
    /// <summary>
    /// Controller for handling user-related actions.
    /// </summary>
    [ApiController]
    [Route("/User")]
    public class UserController : Controller
    {
        private readonly IUserService service;

        /// <summary>
        /// Constructor for the UserController class.
        /// </summary>
        /// <param name="_userService">An instance of the IUserService interface.</param>
        public UserController(IUserService _userService)
        {
            service = _userService;
        }

        /// <summary>
        /// Action method to get jobs associated with a user.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        /// <returns>Returns a list of Job objects associated with the user.</returns>
        [GoogleTokenValidation]
        [HttpGet(Name = "jobs")]
        public List<service.Models.Job> GetUserJobs(Guid userId)
        {
            // Call the GetUserJobs method of the service to retrieve jobs associated with the user.
            return service.GetUserJobs(userId).Result;
        }
    }
}
