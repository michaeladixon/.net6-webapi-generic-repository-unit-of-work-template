using Logic.Repository.Generic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserRepository userRepository) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("GetAllUsers", Summary = "Get All Users")]
    [SwaggerResponse(200, "Users retrieved successfully", typeof(List<UserDto>))]
    public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAllUsers()
    {
        return Ok(await userRepository.GetAllAsync());
    }

    [HttpGet("{id:long}")]
    [SwaggerOperation("GetUserById", Summary = "Get User by ID")]
    [SwaggerResponse(200, "User found", typeof(UserDto))]
    [SwaggerResponse(404, "User not found")]
    public async Task<ActionResult<UserDto>> GetUserById(long id)
    {
        var user = await userRepository.GetByIdAsync(id);
        return user is null ? NotFound() : Ok(user);
    }
}
