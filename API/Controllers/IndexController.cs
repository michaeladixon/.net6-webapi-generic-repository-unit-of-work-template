using Logic.Repository;
using Models;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// Get All Users
        /// </summary>
        /// <remarks>Returns a list of data</remarks>
        /// <response code="200">Users matching filter criteria</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [SwaggerOperation("GetAllUsers")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<UserDto>), description: "Get all data on all users.")]
        public async Task<IActionResult> GetAllUsers()
        {

            return StatusCode(200, await _userRepository.GetAllAsync());
        
        }
    }
}
