using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineFoodDelivery.Data.Models;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Services.Interface;
using System.Security.Claims;

namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUser userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [Authorize(Roles = $"{RoleTypes.Admin},{RoleTypes.SuperAdmin},{RoleTypes.Adminstrator}")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync(User);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = $"{RoleTypes.SuperAdmin},{RoleTypes.Adminstrator},{RoleTypes.Admin}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserByIdAsync((id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RoleTypes.SuperAdmin},{RoleTypes.Adminstrator},{RoleTypes.Admin}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updateUserDto)
        {
            var result = await _userService.UpdateUserAsync(id, updateUserDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RoleTypes.SuperAdmin},{RoleTypes.Adminstrator},{RoleTypes.Admin}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return Ok(result);
        }

        [HttpPost("changepassword")]
        [Authorize(Roles = $"{RoleTypes.Client},{RoleTypes.Adminstrator},{RoleTypes.SuperAdmin}")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            changePasswordDto.UserId = userId;

            var result = await _userService.ChangeUserPassword(changePasswordDto);
            return Ok(result);
        }

    }
}
