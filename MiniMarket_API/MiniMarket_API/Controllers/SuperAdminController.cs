using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model.Entities;
using System.Security.Claims;

namespace MiniMarket_API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class SuperAdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper mapper;
        public SuperAdminController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewAdminAsync([FromBody] CreateUserDto createAdmin)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == typeof(SuperAdmin).Name)
            {
                var adminToCreate = mapper.Map<SuperAdmin>(createAdmin);

                var createdAdmin = await _userService.CreateUser(adminToCreate);
                if (createdAdmin == null)
                {
                    return Conflict("Admin Creation Failed: Email Currently in Use!");
                }

                return Ok(createdAdmin);
            }

            return Forbid();
                
        }
    }
}
