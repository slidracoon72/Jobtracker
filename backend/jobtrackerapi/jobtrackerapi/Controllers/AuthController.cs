using Microsoft.AspNetCore.Mvc;
using service.Interface;
using service.Models;

namespace jobtrackerapi.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related actions.
    /// </summary>
    [ApiController]
    [Route("/Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService service;

        /// <summary>
        /// Constructor for the AuthController class.
        /// </summary>
        /// <param name="_authService">An instance of the IAuthService interface.</param>
        public AuthController(IAuthService _authService)
        {
            service = _authService;
        }

        /// <summary>
        /// Action method to handle authentication using a Google token.
        /// </summary>
        /// <param name="singleSignOn">SingleSignOn object containing authentication information.</param>
        /// <returns>Returns a User object representing the authenticated user.</returns>
        [GoogleTokenValidation]
        [HttpPost]
        public User Index(SingleSignOn singleSignOn)
        {
            // Call the auth method of the service to perform authentication.
            var result = service.auth(singleSignOn).Result;

            // Return the result (a User object representing the authenticated user).
            return result;
        }
    }
}
