using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.DTOs.Requests.Credentials;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model.Entities;
using MiniMarket_API.Model.Enums;
using System.Security.Claims;

namespace MiniMarket_API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISaleOrderService _saleOrderService;

        public UserController(IUserService userService, ISaleOrderService saleOrderService)
        {
            _userService = userService;
            _saleOrderService = saleOrderService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetMyProfileAsync()
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            var getProfile = await _userService.GetUserById(userId);
            if (getProfile == null)
            {
                return NotFound("Profile Wasn't Found");
            }

            return Ok(getProfile);
        }

        [HttpGet("profile/orders")]
        public async Task<IActionResult> GetMyOrdersAsync([FromQuery] OrderStatus? status, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 7)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            var getOrders = await _saleOrderService.GetAllOrdersByUser(userId, status, sortBy, isAscending, pageNumber, pageSize);

            if (getOrders == null || !getOrders.Any())
            {
                return NotFound("You Have No Registered Orders");
            }

            return Ok(getOrders);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] bool? isActive, [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
        {
            var getUsers = await _userService.GetAllUsers(isActive, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            if (getUsers == null || !getUsers.Any())
            {
                return NotFound("No Users Found");
            }

            return Ok(getUsers);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid userId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(SuperAdmin).Name)
            {
                var getUser = await _userService.GetUserById(userId);

                if (getUser == null)
                {
                    return NotFound("User Couldn't be Found");
                }

                return Ok(getUser);
            }

            return Forbid();
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateMyProfileAsync([FromBody] UpdateUserDto updateUser)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            var updatedUser = await _userService.UpdateUser(userId, updateUser);

            if (updatedUser == null)
            {
                return NotFound("User Update Failed: User Couldn't be Found");
            }

            return Ok(updatedUser);
        }

        [HttpPut("profile/password")]
        [Authorize(AuthenticationSchemes = "PasswordRecovery, Bearer")]
        public async Task<IActionResult> SetNewUserPassword([FromBody] NewPasswordRequestDto newPasswordRequest)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            await _userService.SetNewUserPassword(userId, newPasswordRequest);

            //The return will be an empty Ok or a NoContent.
            return Ok();
        }

        [HttpDelete("profile")]
        public async Task<IActionResult> DeactivateMyProfileAsync()
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(SuperAdmin).Name)
            {
                return Forbid();
            }

            await _userService.DeactivateUser(userId);

            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeactivateUserAsync([FromRoute] Guid userId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            var adminId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            if (userRole == typeof(SuperAdmin).Name && adminId != userId)
            {
                await _userService.DeactivateUser(userId);

                return NoContent();
            }

            return Forbid(); 
        }

        [HttpPatch("{userId}")]
        public async Task<IActionResult> RestoreUserAsync([FromRoute] Guid userId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(SuperAdmin).Name)
            {
                var restoredUser = await _userService.RestoreUser(userId);

                if (restoredUser == null)
                {
                    return NotFound("User restoration failed: User couldn't be found.");
                }

                return Ok(restoredUser);
            }

            return Forbid();
        }
    }
}
