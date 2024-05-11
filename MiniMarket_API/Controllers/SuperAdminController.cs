using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Controllers
{
    [Route("api/admin")]
    [ApiController]
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
            var adminToCreate = mapper.Map<SuperAdmin>(createAdmin);

            var createdAdmin = await _userService.CreateUser(adminToCreate);
            if (createdAdmin == null)
            {
                return BadRequest("Could Not Create Admin");
            }
            return Ok(createdAdmin);
        }
    }
}
