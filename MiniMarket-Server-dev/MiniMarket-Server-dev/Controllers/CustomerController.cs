using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.Services.Interfaces;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper mapper;

        public CustomerController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            this.mapper = mapper;
        }

        [HttpPost("Create Customers")]
        public async Task<IActionResult> CreateNewCustomerAsync([FromBody] CreateUserDto createCustomer)
        {
            var customerToCreate = mapper.Map<Customer>(createCustomer);

            var createdCustomer = await _userService.CreateUser(customerToCreate);
            if (createdCustomer == null)
            {
                return BadRequest("Could Not Create Customer");
            }
            return Ok(createdCustomer);
        }
    }
}
